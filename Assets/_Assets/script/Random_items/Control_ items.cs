using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control_items : MonoBehaviour
{
    public GameObject[] items;

    public float impact_force;

    public float radius;

    public GameObject a;

    public bool b;
    private void Start()
    {
        b = false;
    }
    private void Update()
    {
        if(b)
        {
            b=false;
            create_items(a.transform);

        }    
    }








    public void create_items(Transform location_create_items)
    {
        int randomValue = Random.Range(0, items.Length);
        GameObject onitems = Instantiate(items[randomValue], location_create_items.position, Quaternion.identity);
        Rigidbody2D rb = onitems.GetComponent<Rigidbody2D>();
       // rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));
        if(rb != null)
        {
           
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (randomDirection.y < 0) randomDirection.y *= -1;
            randomDirection.y = Random.Range(0, 3);
            randomDirection.y =  Random.Range(1.2f, 3f);
            rb.AddForce(randomDirection * impact_force, ForceMode2D.Impulse);
        }
    }

     

}
