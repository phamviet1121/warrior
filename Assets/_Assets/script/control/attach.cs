using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attach : MonoBehaviour
{
    public Animator anim;
    public bool allow_Attach_bool;

    void Start()
    {
        allow_Attach_bool = true;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //}

    public void on_attach()
    {
        if (allow_Attach_bool)
        {
            anim.SetTrigger("attach");
            allow_Attach_bool = false;
        }

    }
    public void allow_Attach()
    {
        allow_Attach_bool = true;
    }

}
