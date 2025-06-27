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
    //   float previousHorizontal = 0f;
    public bool is_dash;

    public bool onclick_btn_mover;
    public bool onclick_btn_jump;

    void Start()
    {
        inputStart();
        // onjump = true;
        //   is_dash = false;
        horizontal = 0;
        onclick_btn_mover = false;
        onclick_btn_jump = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!attachScript.is_Death && !attachScript.is_durt)
        {
            if (onclick_btn_mover == false)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }



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
            if (!is_dash)
            {

                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            }

            if (onjump)
            {
                anim.SetFloat("mover", Mathf.Abs(horizontal));
            }
            else
            {
                anim.SetFloat("mover", 0f);
            }
            if ((Input.GetKey(KeyCode.Space) && onjump && control.on_Croush == false)||(onclick_btn_jump && onjump && control.on_Croush == false))
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



    public void OnLeftButtonDown()
    {
        onclick_btn_mover = true;
        horizontal = -1f;
    }

    public void OnLeftButtonUp()
    {
        onclick_btn_mover = false;
        horizontal = 0f;
    }

    public void OnRightButtonDown()
    {
        onclick_btn_mover = true;
        horizontal = 1f;
    }

    public void OnRightButtonUp()
    {
        onclick_btn_mover = false;
        horizontal = 0f;
    }
    public void OnJumpButtonDown()
    {
        onclick_btn_jump = true;
    }

    public void OnJumpButtonUp()
    {
        onclick_btn_jump = false;
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
        onjump = true;
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
