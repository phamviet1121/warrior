using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Control_wooden : MonoBehaviour
{
    public bool open_wooden;
    public Animator anim;

    public UnityEvent<Transform> UnityEvent;
    public UnityEvent<Transform, int> UnityEventOne;
    public int[] quantity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        open_wooden = false;
        anim.SetBool("Open", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!open_wooden)
            {
                anim.SetBool("Open", true);
                //UnityEvent.Invoke(transform);
                //UnityEventOne.Invoke(transform, quantity);

                open_wooden = true;
            }



        }
    }

    public void open_wooden_random()
    {
        UnityEvent.Invoke(transform);
    }
    public void open_wooden_One()
    {
        for (int i = 0; i < quantity.Length; i++)
        {
            UnityEventOne.Invoke(transform, quantity[i]);
        }

    }

}
