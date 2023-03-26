using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the object movement
/// </summary>
public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay;
    private Vector3 targetPosition;
    private Vector3 spawnPosition;
    public Vector3 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    } 
    public Vector3 SpawnPosition
    {
        get => spawnPosition;
        set => spawnPosition = value;
    }
    
    private bool isCollisionObject;
    public bool IsCollisionObject
    {
        get => isCollisionObject;
        set => isCollisionObject = value;
    }

    public bool collisionHappened = false;

    private void Start()
    {
        if (isCollisionObject)
        {
            targetPosition = new Vector3(transform.position.x * (-1), 1f, transform.position.z * (-1));
        }

        StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: do not destroy object on collision because sense signalling would stop and the participants would learn (because they then know that they collided with the object)?
        Debug.Log("Collision --> Object Score: 0");
        if (other.gameObject.CompareTag("Player"))
        {
            collisionHappened = true;
            GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);
            
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyDelay);
        
        GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);
        
        Destroy(this.gameObject);
    }
}
