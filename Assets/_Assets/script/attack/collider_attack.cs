using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collider_attack : MonoBehaviour
{
    public bool isattacking;
    public string name_tag;
    public Collider2D cld2d;
    public Collider2D cld2d_;
    public UnityEvent event_dame;

    void Start()
    {
        isattacking = false;
        cld2d.enabled = false;
        cld2d_.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(name_tag))
        {
          

                Debug.Log("da bi tan cong ");
                event_dame.Invoke();
             


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
}
