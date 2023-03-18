using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private float score;
    private ScoreManager scoreManager;

    private void Start()
    {
        score = 0f;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

        float x = Mathf.Clamp(distance, 1f, 4f);
        float score = ((0f - 100f) * (x - 1f) / (4f - 1f)) + 100f;
    }

    public void SetScore()
    {
        if (GetComponent<ObjectMovement>().collisionHappened)
        {
            score = 0f;
        }

        scoreManager.TotalScore += score;
        Debug.Log("Current Total Score: " + scoreManager.TotalScore);
    }
}
