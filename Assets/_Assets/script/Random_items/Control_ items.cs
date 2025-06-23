using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control_items : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] items_vip;
    public GameObject[] items_special;

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
            randomCreate_items(a.transform);

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


    public void randomCreate_items(Transform location_create_items)
    {
        int index_random=Random.Range(0, 3);
       if(index_random>0)
        {
            for(int i = 0; i < index_random; i++) 
            {
                create_items(location_create_items);
            }
        }    
    }
    public void create_items_one(Transform location_create_items, int randomValue)
    {
        GameObject onitems = Instantiate(items[randomValue], location_create_items.position, Quaternion.identity);
        Rigidbody2D rb = onitems.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));
        if (rb != null)
        {

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (randomDirection.y < 0) randomDirection.y *= -1;
            randomDirection.y = Random.Range(0, 3);
            randomDirection.y = Random.Range(1.2f, 3f);
            rb.AddForce(randomDirection * impact_force, ForceMode2D.Impulse);
        }
    }

   




    public void create_itemsVip(Transform location_create_items)
    {
        int randomValue = Random.Range(0, items_vip.Length);
        GameObject onitems = Instantiate(items_vip[randomValue], location_create_items.position, Quaternion.identity);
        Rigidbody2D rb = onitems.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));
        if (rb != null)
        {

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (randomDirection.y < 0) randomDirection.y *= -1;
            randomDirection.y = Random.Range(0, 3);
            randomDirection.y = Random.Range(1.2f, 3f);
            rb.AddForce(randomDirection * impact_force, ForceMode2D.Impulse);
        }
    }

    public void randomCreate_items_vip(Transform location_create_items)
    {
        int index_random = Random.Range(0, 4);
        if (index_random > 0)
        {
            for (int i = 0; i < index_random; i++)
            {
                create_itemsVip(location_create_items);
            }
        }
    }

    public void create_itemsVip_one(Transform location_create_items, int randomValue)
    {
        GameObject onitems = Instantiate(items_vip[randomValue], location_create_items.position, Quaternion.identity);
        Rigidbody2D rb = onitems.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));
        if (rb != null)
        {

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (randomDirection.y < 0) randomDirection.y *= -1;
            randomDirection.y = Random.Range(0, 3);
            randomDirection.y = Random.Range(1.2f, 3f);
            rb.AddForce(randomDirection * impact_force, ForceMode2D.Impulse);
        }
    }
    public void create_items_special_one(Transform location_create_items, int randomValue)
    {
        GameObject onitems = Instantiate(items_special[randomValue], location_create_items.position, Quaternion.identity);
        Rigidbody2D rb = onitems.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));
        if (rb != null)
        {

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            if (randomDirection.y < 0) randomDirection.y *= -1;
            randomDirection.y = Random.Range(0, 3);
            randomDirection.y = Random.Range(1.2f, 3f);
            rb.AddForce(randomDirection * impact_force, ForceMode2D.Impulse);
        }
    }

}
