﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pickUpListener : MonoBehaviour
{
    Character_Movement player;
    SoundManager sounds;
    UIManager score;
    private float restartDelay = 0.5f;
    
    //set up singleton pattern
    private static pickUpListener _instance;
    public static pickUpListener Instance 
    {
        get
        {
            return _instance;
        }
    }

    //only allow one instance of this object
    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        //find player, sound manager, and score board
        player = FindObjectOfType<Player_Movement>();
        sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        score = GameObject.Find("Score").GetComponent<UIManager>();
    }

    //runs when script is enabled
    private void OnEnable()
    {
        //add notifications to list
        Item.objectCollisionNotification += Item_objNotification;
        PowerUp.objectCollisionNotification += powerUp_objNotification;
    }

    private void Item_objNotification(Item gObject)
    {
        Debug.Log(gObject.name);

        //call sound manager function to play item sound
        sounds.PlaySound("PowerUp");

        //call score function to udpate score
        score.AddScore(gObject.scoreValue);
    }

    private void powerUp_objNotification(PowerUp gObject)
    {
        switch(gObject.name)
        {
            //if object name contains multiJump
            case string a when a.Contains("multiJump"):

                //enable multiJump
                GameObject.Find("Player").GetComponent<multiJump>().enabled = true;
                //call sound manager function to play item sound
                sounds.PlaySound("PowerUp");
                player.hasAbility = true;
            break;

            //if object name contains powerAxe
            case string b when b.Contains("powerAxe"):

                //disable multiJump and enable powerAxe
                GameObject.Find("Player").GetComponent<powerAxe>().enabled = true;
                //call sound manager function to play item sound
                sounds.PlaySound("PowerUp");
                player.hasAbility = true;
            break;

            //if object name contains badApple
            case string c when c.Contains("badApple"):

                //if the player has an ability, lose ability
                if(player.hasAbility)
                {
                    //disable all powerups
                    GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;
                    GameObject.Find("Player").GetComponent<multiJump>().enabled = false;
                    player.hasAbility = false;
                }
                else
                {
                    //else, kill player and restart level from last checkpoint
                    player.enabled = false;
                    //restart level and enable player movement
                    deathSceneManager.lastActiveScene = SceneManager.GetActiveScene().name;
                    goToKillPlayerScene();
                    Invoke("ReEnablePlayerMovement", restartDelay);
                }
                
            break;

            //if object name contains BCMODE
            case "BCMODE":
                Debug.Log("BC Mode is enabled");
                score.AddScore(10000);
                sounds.PlaySound("PowerUp");
            break;
        }

        //call score function to udpate score
        score.AddScore(gObject.scoreValue);
    }

    //runs when script is disabled
    private void OnDisable()
    {
        //remove notifications
        Item.objectCollisionNotification -= Item_objNotification;
        PowerUp.objectCollisionNotification -= powerUp_objNotification;
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReEnablePlayerMovement()
    {
        player.enabled = true;
    }

    void goToKillPlayerScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
