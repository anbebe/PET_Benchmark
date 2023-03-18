using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private float score;
    private float scoreOnMovement;
    private bool isCollisionObject;
    private ScoreManager scoreManager;
    private GameObject participant;

    private void Start()
    {
        score = 0f;
        scoreOnMovement = 0f;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        participant = GameObject.FindWithTag("Player");
        isCollisionObject = GetComponent<ObjectMovement>().IsCollisionObject;
    }

    private void Update()
    {
        float distance = Vector3.Distance(participant.transform.position, transform.position);

        float x = Mathf.Clamp(distance, 1f, 4f);
        score = ((0f - 100f) * (x - 1f) / (4f - 1f)) + 100f;
    }

    public void SetCurrentScore()
    {
        scoreOnMovement = score;
        Debug.Log("Preliminary Object Score: " + scoreOnMovement);
    }

    public void AddScoreToTotalScore()
    {
        // if participant collided with the object
        if (GetComponent<ObjectMovement>().collisionHappened)
        {
            score = 0f;
        }
        // if participant did not collide with the object
        else
        {
            // and if object is a collision object
            if (isCollisionObject)
            {
                // set score to the score at the moment of movement (the moment they avoided the collision object)
                score = scoreOnMovement;
            }
            // and if object is not a collision object
            else
            {
                // and if participant moved nevertheless 
                if (participant.GetComponent<GridMovement>().PlayerMovementCounter != 0)
                {
                    // set score to 0 (because object was not a collision object and they should not have moved)
                    score = 0f;
                }
                // if participant did not move
                else
                {
                    // TODO: default score for non-collision objects that the participant correctly ignores?
                    score = 50f;
                }
            }
        }

        scoreManager.TotalScore += score;
        Debug.Log("Current Total Score: " + scoreManager.TotalScore);
    }
}
