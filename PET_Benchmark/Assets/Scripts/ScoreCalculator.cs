using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private float score;
    private float scoreOnMovement;
    private bool isCollisionObject;
    [SerializeField] private ScoreManager scoreManager;
    private GameObject participant;

    private void Start()
    {
        score = 0f;
        scoreOnMovement = 0f;
        scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        participant = GameObject.FindWithTag("Player");
        isCollisionObject = GetComponent<ObjectMovement>().IsCollisionObject;
    }

    /*private void Update()
    {
        // var participantY = participant.transform.position.z;
        // var objectY = transform.position.z;
        // var distance = Vector2.Distance(new Vector2(participant.transform.position.x, participantY), new Vector2(transform.position.x, objectY));
        
        var vectorToTarget = participant.transform.position - transform.position;
        vectorToTarget.y = 0;
        var distanceToTarget = vectorToTarget.magnitude;

        /*switch (distance)
        {
            case > 3f:
                score = 25;
                break;
            case > 2f:
                score = 50;
                break;
            case > 1f:
                score = 75;
                break;
            default:
                score = 100;
                break;
        }#1#

        float x = Mathf.Clamp(distanceToTarget, 0.6f, 4f);
        score = ((0f - 100f) * (x - 0.6f) / (4f - 0.6f)) + 100f;
    }*/

    public void SetCurrentScore()
    {
        var vectorToTarget = participant.transform.position - transform.position;
        vectorToTarget.y = 0;
        var distanceToTarget = vectorToTarget.magnitude;
        
        float x = Mathf.Clamp(distanceToTarget, 1f, 4f);
        score = ((1f - 150f) * (x - 1f) / (4f - 1f)) + 150f;
        score = Mathf.Clamp(score, 1f, 100f);
        
        scoreOnMovement = score;
        Debug.Log("Preliminary Object Score: " + scoreOnMovement);
        Debug.Log("Distance: " + Vector3.Distance(participant.transform.position, transform.position));
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

        //scoreManager.addScore(score);
        ObjectMovement objMoveScript = GetComponent<ObjectMovement>();
        scoreManager.addScoreNew(score, participant.GetComponent<GridMovement>().PlayerMovementCounter, isCollisionObject, objMoveScript.collisionHappened, objMoveScript.SpawnPosition, objMoveScript.TargetPosition, participant.transform.position);
        Debug.Log("Current Total Score: " + scoreManager.TotalScore);
    }
}
