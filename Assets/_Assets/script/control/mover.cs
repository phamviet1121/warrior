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
    void Start()
    {
        inputStart();
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

        anim.SetFloat("mover", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetTrigger("onJump");
        }



    }

    public void inputStart()
    {
        child = transform.GetChild(0);
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
    }



}
