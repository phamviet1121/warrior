using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator anim;
    private Transform child;
    public float jump;
    public bool onjump;
    public Control control;
    public bool left_rihgt = true;
    private attach attachScript;
    public float horizontal;
    void Start()
    {
        inputStart();
        onjump = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (!attachScript.is_Death && !attachScript.is_durt)
        {
             horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            if (horizontal < 0)
            {
                left_rihgt = false;
                child.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (horizontal > 0)
            {
                left_rihgt = true;
                child.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (onjump)
            {
                anim.SetFloat("mover", Mathf.Abs(horizontal));
            }
            else
            {
                anim.SetFloat("mover", 0f);
            }

            if (Input.GetKey(KeyCode.Space) && onjump && control.on_Croush == false)
            {
                onjump = false;
                rb.velocity = new Vector2(rb.velocity.x, jump);
                anim.SetTrigger("onJump");
               // Debug.Log("anim.SetTrigger(\"onJump\")");
            }
        }
        else
        {
            anim.SetFloat("mover", 0f);
            rb.velocity = new Vector2(0f * speed, rb.velocity.y);
        }
    }

    public void inputStart()
    {
        child = transform.GetChild(0);
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
        attachScript = child.GetComponent<attach>();

        if (child.transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            left_rihgt = false;
        }
        else if (child.transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            left_rihgt = true;
        }
    }

    public void allow_jump()
    {
        anim.ResetTrigger("onJump");
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
