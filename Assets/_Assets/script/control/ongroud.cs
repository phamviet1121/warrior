using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ongroud : MonoBehaviour
{
    public UnityEvent On_Ground;

  
    public UnityEvent Off_Ground;

    private int groundCount = 0; // Đếm số ground đang chạm

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundCount++;
            if (groundCount == 1) // Vừa mới chạm ground đầu tiên
            {
                On_Ground.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundCount--;
            if (groundCount <= 0) // Không còn chạm ground nào nữa
            {
                groundCount = 0; // Đảm bảo không xuống âm
                Off_Ground.Invoke();
            }
        }
    }
}
