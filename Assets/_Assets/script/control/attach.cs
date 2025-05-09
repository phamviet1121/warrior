using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attach : MonoBehaviour
{
    public Animator anim;
    public bool allow_Attach_bool;
   
    public Collider2D Collider2D_1;
    public Collider2D Collider2D_2;



    void Start()
    {
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        allow_Attach_bool = true;
    }

 




    public void on_attach()
    {
        if (allow_Attach_bool)
        {
            anim.SetTrigger("attach");
            allow_Attach_bool = false;
        }       
    }

    public void on_Croush()
    {

        anim.SetBool("onCroush", true);
        Collider2D_1.enabled = false;
        Collider2D_2.enabled = true;
        allow_Attach_bool = false;
    }
    public void off_Croush()
    {
        anim.SetBool("onCroush", false);
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        allow_Attach_bool = true;
    }

    public void allow_Attach()
    {
        allow_Attach_bool = true;
    }


}
