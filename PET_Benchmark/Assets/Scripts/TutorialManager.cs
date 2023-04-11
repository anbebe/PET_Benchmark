using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Random = UnityEngine.Random;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float delay;
    [SerializeField] private float preparationDelay;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private Image errorScreen;

    private bool spawnObjects;
    private bool paused;

    public bool tutorialInProgress;

    private GameObject player;
    private Coroutine coroutine;

    private bool t1_InProgress = false;
    private bool t2_InProgress = false;

    private void Start()
    {
        errorScreen.GetComponent<Image>().enabled = false;
        scoreText.text = "Score: 0";
        spawnObjects = false;
        paused = false;
        tutorialInProgress = true;
        player = GameObject.FindWithTag("Player");
        //coroutine = SpawnObjects();

    }

    private void Update()
    {
        scoreText.text = "Score: " + Math.Round(scoreManager.TotalScore);

       if (SteamVR_Actions.movement_Tutorial1.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.T))
        {
            t2_InProgress = false;
            if (t1_InProgress)
            {
                StopCoroutine(coroutine);
                t1_InProgress = false;
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                t1_InProgress = true;
                StartTutorialPartOne();
            }
        }

        if (SteamVR_Actions.movement_Tutorial2.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.I))
        {
            t1_InProgress = false;
            if (t2_InProgress)
            {
                StopCoroutine(coroutine);
                t2_InProgress = false;
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                t2_InProgress = true;
                StartTutorialPartTwo();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                UnpauseTutorial();
            }
            else
            {
                PauseTutorial();
            }
        }

    }

    private void StartTutorialPartOne()
    {
        Debug.Log("Start Tutorial Part 1");
        obj.GetComponent<MeshRenderer>().enabled = true;
        spawnObjects = true;
        coroutine = StartCoroutine(SpawnObjects());
    }

    private void StartTutorialPartTwo()
    {
        Debug.Log("Start Tutorial Part 2");
        obj.GetComponent<MeshRenderer>().enabled = false;
        scoreManager.TotalScore = 0f;
        spawnObjects = true;
        coroutine = StartCoroutine(SpawnObjects());
    }

    private void PauseTutorial()
    {
        Debug.Log("Pause Tutorial");
        Destroy(transform.GetChild(0).gameObject);
        spawnObjects = false;
        paused = true;
    }

    private void UnpauseTutorial()
    {
        Debug.Log("Resume Tutorial");
        spawnObjects = true;
        paused = false;
    }
    
    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(preparationDelay);
        
        while (spawnObjects)
        {
            yield return new WaitForSeconds(1f);
            errorScreen.GetComponent<Image>().enabled = false;
                
            // wait before instantiating the next object
            yield return new WaitForSeconds(delay);

            // reset playerMovementCounter for each object
            player.GetComponent<GridMovement>().PlayerMovementCounter = 0;

            Vector2 randomPoint = Random.insideUnitCircle.normalized * 4f;
            Vector3 pos = new Vector3(randomPoint.x, 1f, randomPoint.y);
            GameObject instantiatedObj = Instantiate(obj, pos, Quaternion.identity, this.transform);
            
            MovementScript objMovementScript;
            if (ExperimentManager.isTutorial)
            {
                objMovementScript = instantiatedObj.GetComponent<TutorialObjectMovement>();
            }
            else
            {
                objMovementScript = instantiatedObj.GetComponent<ObjectMovement>();
            }
                
            //give the instantiated object some more info that is later saved to CSV
            objMovementScript.SpawnPosition = pos;
            objMovementScript.IsCollisionObject = true;
        }

        yield return new WaitUntil(() => spawnObjects);
    }

    public void ShowErrorScreen(bool turnOn)
    {
        errorScreen.GetComponent<Image>().enabled = turnOn;
    }
}
