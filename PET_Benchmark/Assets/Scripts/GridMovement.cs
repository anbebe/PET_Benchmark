using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Valve.VR;

public class GridMovement : MonoBehaviour
{
    public Vector2 trackpad;
    public LayerMask gridMask;
    
    //public GameObject[] grid = new GameObject[9];
    private int playerMovementCounter;

    private bool hasMoved = false; 
    public int PlayerMovementCounter
    {
        get => playerMovementCounter;
        set => playerMovementCounter = value;
    }

    private GameObject spawnManager;

    private void Start()
    {
        //moveAction.AddOnAxisListener(DoMovement, handType);
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
    private void updateInput()
    {
        trackpad = SteamVR_Actions.movement_TakeStep.GetAxis(SteamVR_Input_Sources.Any);
       // Debug.Log("A Down: " + SteamVR_Actions.movement_Tutorial1.GetStateDown(SteamVR_Input_Sources.Any));
       // Debug.Log(SteamVR_Input.GetStateDown("Tutorial1", SteamVR_Input_Sources.Any));
        //Debug.Log(trackpad);
    }

    /*public void DoMovement(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        Debug.Log(axis);
    }*/

    // Update is called once per frame
    void Update()
    {
        updateInput();

        if (hasMoved == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, 0.0f, transform.position.z),
                    new Vector3(trackpad.x, 0.0f, trackpad.y), out hit, 2.0f, gridMask))
            {
                //Debug.Log(hit.transform.gameObject.name);
                this.transform.position = hit.transform.position + new Vector3(0,1.1f,0);
                hasMoved = true;
                StartCoroutine(Delay());
            }
        }
        
        /*
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
        }*/
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        hasMoved = false;
    }
    
}
