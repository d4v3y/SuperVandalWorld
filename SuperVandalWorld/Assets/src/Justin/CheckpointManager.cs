﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    //Singleton - only one checkpoint instance can exist
    private static CheckpointManager instance;
    public Vector2 lastCheckPointPos;

    //Variable to see if next level is loaded
    public LevelLoader lvlLoaded;

    void Awake()
    {
        //Keep current instance between scene loads
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            //if an instance already exists, destroy it.
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //Get LevelLoader Script
        lvlLoaded = GameObject.FindObjectOfType<LevelLoader>();

        if(SceneManager.GetActiveScene().name != "DeathScene")
        {
            //Upon loading to the next level
            if(lvlLoaded.nxtLevel)
            {
                //reset checkpoint position to beginning of next level
                lastCheckPointPos = new Vector2(0,0);
                //Set the state of the next level loaded back to false to indicate nxlLevel = current level
                lvlLoaded.SetToCurrentLevel();
            }
        }
        
    }
}
