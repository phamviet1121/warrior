using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class attach : MonoBehaviour
{
    public Animator anim;
    public bool allow_Attach_bool;
    public bool Croush_Attach_bool;
    private bool canSlideAttack = true;

    public Collider2D Collider2D_1;
    public Collider2D Collider2D_2;

    public float moveDistance = 2f;

    void Start()
    {
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        allow_Attach_bool = true;
    }






    public void on_attach()
    {
        if (allow_Attach_bool && Croush_Attach_bool)
        {
            anim.SetTrigger("attach");
            allow_Attach_bool = false;
        }

    }
    public void on_slide_attack()
    {


        if (canSlideAttack)
        {
            canSlideAttack = false;
           
            anim.SetTrigger("slide_attack");
            StartCoroutine(SlideAttackCooldown());
        }

    }
    private IEnumerator SlideAttackCooldown()
    {
        // canSlideAttack = false;
        yield return new WaitForSeconds(2f);
        canSlideAttack = true;
    }




    public void on_Croush()
    {

        anim.SetBool("onCroush", true);
        Collider2D_1.enabled = false;
        Collider2D_2.enabled = true;
        Croush_Attach_bool = false;
    }
    public void off_Croush()
    {
        anim.SetBool("onCroush", false);
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        Croush_Attach_bool = true;
    }

    public void allow_Attach()
    {
        allow_Attach_bool = true;
    }


}
