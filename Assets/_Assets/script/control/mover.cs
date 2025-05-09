using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mover : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator anim;
    private Transform child;
    public float jump;
    public bool onjump;
    void Start()
    {
        inputStart();
        onjump = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (horizontal > 0)
        {
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            child.transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        if (onjump)
        {
            anim.SetFloat("mover", Mathf.Abs(horizontal));
        }
        else
        {
            anim.SetFloat("mover", 0f);
        }


        if (Input.GetKeyDown(KeyCode.Space)&& onjump)
        {
            onjump = false;
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetTrigger("onJump");
            Debug.Log("anim.SetTrigger(\"onJump\")");
        }


        //if (Input.GetKey(KeyCode.S))
        //{
        //    anim.SetBool("onCroush",true);
        //}else
        //{
        //    anim.SetBool("onCroush", false);
        //}


    }

    public void inputStart()
    {
        child = transform.GetChild(0);
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
    }

    public void allow_jump()
    { anim.ResetTrigger("onJump");
        if (!onjump)
        {
            onjump = true;
        }
    }
    public void Not_allow_jump()
    {
       
        if (onjump)
        {
            onjump = false;
        }
    }

}
