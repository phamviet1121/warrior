using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collider_attack : MonoBehaviour
{
    public bool isattacking;
    public string name_tag;
    public UnityEvent event_dame;

    void Start()
    {
        isattacking = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(name_tag))
        {
            Debug.Log("co cham ko  ");
            if (isattacking)
            {

                Debug.Log("da bi tan cong ");
                event_dame.Invoke();
                isattacking = false;
            }    


        }
    }


    public void on_isattacking()
    {
        Debug.Log("co tan cong ko ha  ");
        isattacking = true;
    }
    public void off_isattacking()
    {
        isattacking = false;
    }


}
