using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Control_level : MonoBehaviour
{

    public float maxIndexLevel;
    public float currentIndexLevel;
    public int indexlevel=1;

    public UnityEvent event_level_up;
  
    public void currentIndexLevel_Up(float index)
    {
        currentIndexLevel += index;
        if (currentIndexLevel >= maxIndexLevel)
        {
            event_level_up.Invoke();
            indexlevel += 1;
            maxIndexLevel += maxIndexLevel * (50f / 100f);
        }

    }

   

}
