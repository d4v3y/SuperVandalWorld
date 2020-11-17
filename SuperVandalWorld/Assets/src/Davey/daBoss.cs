﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daBoss : MonoBehaviour
{
    public int health = 400;

    // Retrieve player movement
    public float restartWait = 1f;
    Player_Movement playerMvmnt;
    protected bool playerAlive = false;

    // Choose the enemy and player ridgid body
    private Rigidbody2D rb;
    private GameObject player;

    Renderer rnr;
    Vector3 move = Vector3.zero;        // Variable to change x-axis value

    // Set position of enemy at launch
    [HideInInspector]
    public Vector2 initPos;

    float someScale;
    public float fallMultiplier = 2.5f;

    public HealthBar healthBar;

    public GameObject bul;
    public float bulletForce = 3f;

    void Start() 
    {
        healthBar.SetMaxHealth(health);

        playerAlive = true;
        playerMvmnt = FindObjectOfType<Player_Movement>();

        someScale = transform.localScale.x; // assuming this is facing right
        initPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        rnr = GetComponent<Renderer>();
        // transform.localScale = new Vector2(-someScale, transform.localScale.y);
    }

    // Track and follow player location
    void Update() 
    {
        // Check if the enemy is visibly on the scene
        if (rnr.isVisible) 
        {

        }

        // Control falling speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        if (playerAlive == true)
        {
            if (collision.collider.tag == "Player")
            {
                // see if the obect is futher left/right or top/bottom
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {

                    if(direction.x > 0)
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss on your right.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                    else
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss on your left.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                
                }
                else
                {

                    if(direction.y > 0)
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss falling on you.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                    else
                    {
                        health -= 50;
                        healthBar.SetHealth(health);
                        Debug.Log("Boss health = " + health);

                        if (health == 0)
                        {
                            Debug.Log("You defeated the boss!");
                            
                            playerMvmnt.enabled = true;
                            score.AddScore(1000);
    
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReEnablePlayerMovement()
    {
        playerMvmnt.enabled = true;
    }
}
