using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_attack_one : MonoBehaviour
{
    public float Damage;
    public bool is_collider;
    private void Start()
    {
        is_collider = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (is_collider)
            {
                HealthSystem health = collision.GetComponent<HealthSystem>();
                if (health != null)
                {

                    health.TakeDamage(Damage);
                }
                attach attach = collision.GetComponent<attach>();
                if (attach != null)
                {

                    attach.hurt_ondamage(transform.position);
                }

                StartCoroutine(time_collider());
            }

        }
    }
    IEnumerator time_collider()
    {
        is_collider = false;
        yield return new WaitForSeconds(2f);
        is_collider = true;
    }
}


