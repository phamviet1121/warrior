using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collider_saver_scene : MonoBehaviour
{
    public UnityEvent event_save_next_scene;
    bool isevent_save_next_scene;
    private void Start()
    {
        isevent_save_next_scene = false;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isevent_save_next_scene)
            {
               
                event_save_next_scene.Invoke();
                isevent_save_next_scene = true;
            }

        }
    }
}
