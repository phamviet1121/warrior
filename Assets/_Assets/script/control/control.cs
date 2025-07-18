using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    private GameObject child;
    private attach attachScript;
    private EnergySystem energySystem;
    private Mover moverScript;
    public bool on_Croush;
    private bool left_right;
    private bool jump;
    public bool is_dash;
    public float _mana;

    public bool onclick_btn_K;
    public bool onclick_btn_U;
    public bool onclick_btn_S;

    void Start()
    {
        input_start();
    }

    public void input_start()
    {
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();
        attachScript = child.GetComponent<attach>();
        moverScript = transform.GetComponent<Mover>();
        energySystem = child.GetComponent<EnergySystem>();
        onclick_btn_K = false;
        onclick_btn_U = false;
        onclick_btn_S = false;
    }

    // Update is called once per frame
    void Update()
    {



        if (!attachScript.is_Death && !attachScript.is_durt)
        {



            left_right = moverScript.left_rihgt;

            jump = moverScript.onjump;
            if (Input.GetKey(KeyCode.K)|| onclick_btn_K)
            {
                if (on_Croush)
                {
                    onClick_slide_attack();
                }
                else
                {
                    onClick_Attach();
                }


            }

            if ((Input.GetKeyDown(KeyCode.U) || onclick_btn_U) && jump && !on_Croush && moverScript.horizontal == 0 && attachScript.allow_Attach_bool)
            {
                if (energySystem.currentEnergy >= _mana)
                {
                    energySystem.TakeMana(_mana);
                    onClick_dash_attack();
                }

            }



            if ((Input.GetKey(KeyCode.S)|| onclick_btn_S) && jump == true)
            {
                moverScript.is_dash = true;
                on_Croush = true;
                onClick_Croush();


            }
            else
            {
                on_Croush = false;
                offClick_Croush();
            }

        }

        is_dash = attachScript.is_dash;
        moverScript.is_dash = is_dash;
    }

    public void OnDownButtonDown()
    {
        onclick_btn_S = true;
    }

    public void OnDownButtonUp()
    {
        onclick_btn_S = false;
    }
    public void OnKButtonDown()
    {
        onclick_btn_K = true;
    }

    public void OnKButtonUp()
    {
        onclick_btn_K = false;
    }

    public void OnUButtonDown()
    {
        onclick_btn_U = true;
    }

    public void OnUButtonUp()
    {
        onclick_btn_U = false;
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
            //Debug.Log("1");
            attachScript.on_attach();
        }

    }

    public void onClick_slide_attack()
    {
        //attach attachScript = child.GetComponent<attach>();
        if (attachScript != null)
        {
            //Debug.Log("1");
            attachScript.on_slide_attack(left_right, rb);
        }

    }
    public void onClick_dash_attack()
    {
        //attach attachScript = child.GetComponent<attach>();
        if (attachScript != null)
        {
            //Debug.Log("1");
            attachScript.on_Dash_attach(left_right, rb);
        }

    }


    public void on_hurt()
    {
        if (attachScript != null)
        {
            //Debug.Log("1");
            attachScript.on_hurt(left_right);
        }

    }

}
