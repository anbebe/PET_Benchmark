using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GridMovement : MonoBehaviour
{
    public GameObject[] grid = new GameObject[9];
    private int playerMovementCounter;
    public int PlayerMovementCounter
    {
        get => playerMovementCounter;
        set => playerMovementCounter = value;
    }

    private GameObject spawnManager;

    private void Start()
    {
        playerMovementCounter = 0;
        if (ExperimentManager.isTutorial)
        {
            spawnManager = GameObject.Find("TutorialManager");
        }
        else
        {
            spawnManager = GameObject.Find("SpawnManager");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (Input.GetKeyDown("[" + (i+1) + "]"))
            {
                this.transform.position = grid[i].transform.position + new Vector3(0,1.1f,0);
                playerMovementCounter += 1;
                Debug.Log("Current Player Movement Counter: " + playerMovementCounter);
                ScoreCalculator calc = spawnManager.GetComponentInChildren<ScoreCalculator>();
                if (calc != null)
                {
                    calc.Invoke("SetCurrentScore", 0f);
                }
            }
        }
        
        
    }
}
