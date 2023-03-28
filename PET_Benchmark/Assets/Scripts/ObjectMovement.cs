using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the object movement
/// </summary>
public class ObjectMovement : MonoBehaviour
{
    private TutorialManager tutorialManager;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay;
    private Vector3 targetPosition;
    private Vector3 spawnPosition;

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

    private void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
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
        // TODO: do not destroy object on collision because sense signalling would stop and the participants would learn (because they then know that they collided with the object)?
        Debug.Log("Collision --> Object Score: 0");
        if (other.gameObject.CompareTag("Player"))
        {
            collisionHappened = true;
            GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);
            
            if (tutorialManager.tutorialInProgress)
            {
                //source.PlayOneShot(errorClip);
                tutorialManager.ShowErrorScreen();
            }

            StartCoroutine(DestroyObject(1f));
        }
    }

    private IEnumerator DestroyObject(float destroydly)
    {
        yield return new WaitForSeconds(destroydly);

        if (!tutorialManager.tutorialInProgress)
        {
            GetComponent<ScoreCalculator>().Invoke("AddScoreToTotalScore", 0f);
        }
        
        Destroy(this.gameObject);
    }
}
