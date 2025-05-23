using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * 5;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Wall") || collision.collider.CompareTag("WoodenBox"))
        {  
           Destroy(gameObject);
        }
    }
}
