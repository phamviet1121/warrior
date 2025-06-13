using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_items : MonoBehaviour
{
    public float time;
    public float runtime;
    public float nubur;
    public int index;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        runtime += Time.deltaTime;
        if (runtime >= time)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
            if (health != null)
            {
                if (index == 0)
                {
                    health.Take_healing(nubur); // Ideally use a Heal method
                }
                else if (index == 1)
                {
                    health.IncreaseMaxHealth(nubur); // Ideally use a Heal method
                }
                Destroy(gameObject); // Optionally destroy after pickup
            }
        }
    }
}
