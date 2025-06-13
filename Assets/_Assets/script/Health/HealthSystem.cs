using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject damageTextPrefab;
    public Transform textSpawnPoint;
    public Transform canvasTransform;

    public UnityEvent event_die;
    public UnityEvent event_hurt;



    //private float timeSinceLastDamage = 0f;
    //private float timeToHideSlider = 5f;

    void Start()
    {

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    //void Update()
    //{
    //    // Nếu không nhận sát thương trong 5 giây, ẩn thanh máu
    //    if (timeSinceLastDamage > timeToHideSlider)
    //    {
    //        healthSlider.gameObject.SetActive(false); // Ẩn thanh máu
    //    }
    //    else
    //    {
    //        timeSinceLastDamage += Time.deltaTime; // Cập nhật thời gian trôi qua
    //    }
    //}
    void LateUpdate()
    {
        // Giữ scale dương
        Vector3 scale = healthSlider.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        healthSlider.transform.localScale = scale;

        // Giữ nguyên rotation hoặc luôn quay về camera
        healthSlider.transform.forward = Camera.main.transform.forward;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        ShowDamageText(damage); // gọi text

        event_hurt.Invoke();
      
       // timeSinceLastDamage = 0f;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Take_healing( float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;
        ShowHealingText(healing);
    }    


    void Die()
    {
        Debug.Log("Chết rồi!");
        event_die.Invoke();
        // Xử lý chết ở đây (ẩn object, respawn, game over...)
    }
    void ShowDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            GameObject textObj = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity, canvasTransform);
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = damage.ToString("F0"); // hiện sát thương là số nguyên
            }
        }
    }

    void ShowHealingText(float healing)
    {
        if (damageTextPrefab != null)
        {
            GameObject textObj = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity, canvasTransform);
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            { 
                tmp.color = Color.green;
                tmp.text =" + " + healing.ToString("F0"); // hiện sát thương là số nguyên
               
            }
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;

        // Nếu muốn tăng máu hiện tại theo lượng mới
       
            //currentHealth += amount;
        

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cập nhật UI
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Gọi hồi máu hiển thị text (tuỳ chọn)
       
            ShowHealingText(amount);
        
    }
}
