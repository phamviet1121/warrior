using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextEffect : MonoBehaviour
{
    public float floatSpeed = 50f;
    public float fadeDuration = 1f;
    private TextMeshProUGUI tmpText;
    private Color originalColor;
    private float timer;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
        timer = 0f;
    }

    void Update()
    {
        // Di chuyển text bay lên
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // Làm mờ dần
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Xoá sau khi mờ hết
        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }

    }
}
