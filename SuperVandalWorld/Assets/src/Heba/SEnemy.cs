﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemy : MonoBehaviour
{
    public float speed;    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVelocity;
    public float curTime;
    public float moveTime = 3;
    public float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
       animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= moveTime)
        {
            direction *= -1;
            curTime = 0;
        }
        moveVelocity = new Vector2(direction, 0) * speed;
        
    }

     void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    
}
