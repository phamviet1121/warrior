using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_attack : MonoBehaviour
{
    public float Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem health = collision.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(Damage);
            }
            attach attach = collision.GetComponent<attach>();
            if (attach != null)
            {
                Debug.Log("1111111");
                attach.hurt_ondamage(transform.position);
            }
            else
            {
                Debug.Log("2222222");
            }
        }
    }
}
