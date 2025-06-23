using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_point : MonoBehaviour
{
    public Control_start_player control_Start_Player;
    public bool isLocation;
    private void Start()
    {
        isLocation = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!isLocation)
            {
                control_Start_Player.saver_location_replay(transform);
                isLocation =true;
            }    
        }
    }


}
