using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detroy_fire : MonoBehaviour
{
    public float time;
    private float runtime;
    private void Start()
    {
        runtime = time;
    }
    private void Update()
    {
        runtime-=Time.deltaTime;
        if(runtime <= 0 )
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("chem"))
        {
            Destroy(gameObject);
        }

    }

}
