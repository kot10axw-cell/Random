using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController: MonoBehaviour
{
    Rigidbody2D rb2d;
    float speed = 5f;
    Vector2 direction;
    [SerializeField]
    [Range(0.5f, 2f)]
    float speedy;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        
           // додаткове завдання
         if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 10f);
        }
        
    }

    
    void FixedUpdate()
    {
        // rb2d.velocity = direction * speed;
         rb2d.linearVelocity = new Vector2(direction.x * speed, rb2d.linearVelocity.y);
         transform.eulerAngles = new Vector3(0, 90 + -90*Mathf.Sign (direction.x), 0);
    }

    
}
