using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject child;
    private attach attachScript;


    void Start()
    {
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
        attachScript = child.GetComponent<attach>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            onClick_Attach();
        }

        //if (Input.GetKey(KeyCode.S))
        //{
        //    onClick_Croush();
        //}
        //else
        //{
        //    offClick_Croush();
        //}


    }

    public void onClick_Croush()
    {
        
        if (attachScript != null)
        {
           
            attachScript.on_Croush();
        }

    }

    public void offClick_Croush()
    {

        if (attachScript != null)
        {
         
            attachScript.off_Croush();
        }

    }

    public void onClick_Attach()
    {
        //attach attachScript = child.GetComponent<attach>();
        if (attachScript != null)
        {
            Debug.Log("1");
            attachScript.on_attach();
        }

    }
}
