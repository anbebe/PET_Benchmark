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

    private void Start()
    {
        errorScreen.GetComponent<Image>().enabled = false;
        scoreText.text = "Score: 0";
        spawnObjects = false;
        paused = false;
        tutorialInProgress = true;
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
            errorScreen.GetComponent<Image>().enabled = false;

            Vector2 randomPoint = Random.insideUnitCircle.normalized * 4f;
            Vector3 pos = new Vector3(randomPoint.x, 1f, randomPoint.y);
            Instantiate(obj, pos, Quaternion.identity, this.transform);

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(() => spawnObjects);
    }

    public void ShowErrorScreen()
    {
        errorScreen.GetComponent<Image>().enabled = true;
    }
}
