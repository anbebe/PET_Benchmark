using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the object movement
/// </summary>
public class ObjectMovement : MonoBehaviour, MovementScript
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay;
    private Vector3 targetPosition;
    private Vector3 spawnPosition;
    private GameObject player;

    private AudioSource source;
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

    public bool CollisionHappened
    {
        get => collisionHappened;
        set => collisionHappened = value;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = gameObject.GetComponent<AudioSource>();
        if (isCollisionObject)
        {
            targetPosition = new Vector3(transform.position.x * (-1), 1f, transform.position.z * (-1));
        }

        StartCoroutine(DestroyObject(destroyDelay));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision --> Object Score: 0");
            collisionHappened = true;
            //GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);

            //TODO: change anything after collision?
        }
    }

    private IEnumerator DestroyObject(float destroydly)
    {
        yield return new WaitForSeconds(destroydly);

        if (ExperimentManager.currentMode == ExperimentManager.ExperimentMode.Tactile)
        {
            GameObject.FindGameObjectWithTag("Vest").GetComponent<VibrationTest>().ClearMotors();
        }
        GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);

        Destroy(this.gameObject);

        player.transform.position = new Vector3(0f, 1.1f, 0f);
    }
}
