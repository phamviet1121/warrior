using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    //[Header("UI Elements")]
    public Slider energySlider;
    public GameObject damageTextPrefab;
    public Transform textSpawnPoint;
    public Transform canvasTransform;

   // [Header("Mana Settings")]
    public float maxEnergy = 100f;
    public float currentEnergy;

    void Start()
    {
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
    }

    void LateUpdate()
    {
        // Giữ scale dương theo trục x
        Vector3 scale = energySlider.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        energySlider.transform.localScale = scale;

        // Luôn quay mặt về phía camera
        energySlider.transform.forward = Camera.main.transform.forward;
    }

    /// <summary>
    /// Gây tiêu hao năng lượng (giảm Mana)
    /// </summary>
    public void TakeMana(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energySlider.value = currentEnergy;

        ShowPopupText("-" + amount.ToString("F0"), Color.white);
    }

    /// <summary>
    /// Hồi năng lượng (tăng Mana)
    /// </summary>
    public void RestoreMana(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energySlider.value = currentEnergy;

        ShowPopupText("+" + amount.ToString("F0"), Color.yellow);
    }

    /// <summary>
    /// Tăng giới hạn Mana tối đa
    /// </summary>
    public void IncreaseMaxMana(float amount)
    {
        maxEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;

        ShowPopupText("+" + amount.ToString("F0"), Color.cyan);
    }

    /// <summary>
    /// Hiển thị popup text (gây hoặc hồi mana)
    /// </summary>
    void ShowPopupText(string text, Color color)
    {
        if (damageTextPrefab != null && canvasTransform != null && textSpawnPoint != null)
        {
            GameObject textObj = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity, canvasTransform);
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.color = color;
                tmp.text = text;
            }
        }
    }
}
