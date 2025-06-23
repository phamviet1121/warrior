using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class collider_attack : MonoBehaviour
{
    public bool isattacking;
    public bool is_attacking;
    public bool isattacking_;
    public string name_tag;

    public float damageAmount = 10f;
    public float damageAmount_ = 10f;
    public float _damageAmount = 10f;
    public float Damage;

    public Collider2D cld2d;
    public Collider2D cld2d_;

    public GameObject damageTextPrefab;
    public Transform textSpawnPoint;
    public Transform canvasTransform;

    //public UnityEvent event_dame;

    void Start()
    {
        isattacking = false;
        is_attacking = false;
        isattacking_ = false;

        Damage = damageAmount;
        isattacking = false;
        cld2d.enabled = false;
        cld2d_.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(name_tag))
        {

            // 
            is_Damage();
            // Kiểm tra xem object bị va chạm có HealthSystem không
            HealthSystem health = collision.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(Damage);
                // event_dame.Invoke();

            }



        }
    }



    public void on_isattacking()
    {

        cld2d.enabled = true;
        cld2d_.enabled = false;
    }
    public void off_isattacking()
    {
        cld2d.enabled = false;
    }
    public void on_isattacking_()
    {
        cld2d_.enabled = true;
        cld2d.enabled = false;
    }
    public void off_isattacking_()
    {
        cld2d_.enabled = false;
    }

    public void is_Damage()
    {
        if (isattacking)
        {
            Damage = damageAmount;
        }
        else if (is_attacking)
        {
            Damage = damageAmount_;
        }
        else if (isattacking_)
        {
            Damage = _damageAmount;
        }
    }

    public void Increase_Damage(float nuber)
    {
        damageAmount += damageAmount * (nuber / 100f);
        _damageAmount += _damageAmount * (nuber / 100f);
        damageAmount_ += damageAmount_ * (nuber / 100f);

        if (damageTextPrefab != null)
        {
            GameObject textObj = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity, canvasTransform);
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.color = Color.red;
                tmp.text = " + " + nuber.ToString("F0")+" % "; // hiện sát thương là số nguyên

            }
        }




    }
}
