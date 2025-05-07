using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class control : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject child;
   
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            onClick_Attach();
        }    
    }

    public void onClick_Attach()
    {
        attach attachScript = child.GetComponent<attach>();
        if (attachScript != null)
        {
            Debug.Log("1");
            attachScript.on_attach();
        }
       
    }
}
