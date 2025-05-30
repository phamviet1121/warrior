using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover_player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
  

 
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
