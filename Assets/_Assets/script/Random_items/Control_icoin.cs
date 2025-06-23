using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Control_icoin : MonoBehaviour
{
    public float icoin;
    public float revive;
    public float un_revive;

    public void increase_icoin(float nuber)
    {
        icoin += nuber;
    }    

    public void increase_revive(float nuber)
    {
        revive += nuber;
    }   
    public void replay_revive()
    {
        if(revive>= un_revive)
        {
            revive -= un_revive;
        }
        
    }
}
