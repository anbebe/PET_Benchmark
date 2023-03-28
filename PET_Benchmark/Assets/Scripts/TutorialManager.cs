using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    private void Start()
    {
        errorScreen.GetComponent<Image>().enabled = false;
        scoreText.text = "Score: 0";
        spawnObjects = false;
        paused = false;
        tutorialInProgress = true;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        scoreText.text = "Score: " + Math.Round(scoreManager.TotalScore);
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartTutorialPartOne();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StopCoroutine(SpawnObjects());
            StartTutorialPartTwo();
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
        spawnObjects = true;
        StartCoroutine(SpawnObjects());
    }

    private void StartTutorialPartTwo()
    {
        Debug.Log("Start Tutorial Part 2");
        obj.GetComponent<MeshRenderer>().enabled = false;
        scoreText.text = "Score: 0";
        spawnObjects = true;
        StartCoroutine(SpawnObjects());
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
            
            // wait until participant returns to center; TODO: does not seem to work suddenly
            yield return new WaitUntil(() =>
                player.transform.position == new Vector3(0, 1.1f, 0));
                
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

    public void ShowErrorScreen()
    {
        errorScreen.GetComponent<Image>().enabled = true;
    }
}
