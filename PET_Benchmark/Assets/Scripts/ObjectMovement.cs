using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay;
    private Vector3 targetPosition;
    private bool isCollisionObject;
    
    private void Start()
    {
        isCollisionObject = Random.value > .25; // random True or False generator, with True being more probable
        
        if (isCollisionObject)
        {
            targetPosition = new Vector3(transform.position.x * (-1), 1f, transform.position.z * (-1));
        }
        else // let object move without colliding with the player; target positions hard-coded --> TODO: easier?
        {
            switch (transform.position.z)
            {
                case < -3f when transform.position.x >= 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(-4f, 1f, 0f) : new Vector3(4f, 1f, 4f);
                    break;
                case < -3f:
                    targetPosition = Random.value > 0.5 ? new Vector3(4f, 1f, 0f) : new Vector3(-4f, 1f, 4f);
                    break;
                case > 3f when transform.position.x >= 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(-4f, 1f, 0f) : new Vector3(4f, 1f, -4f);
                    break;
                case > 3f:
                    targetPosition = Random.value > 0.5 ? new Vector3(4f, 1f, 0f) : new Vector3(-4f, 1f, -4f);
                    break;
                case < 0f when transform.position.x >= 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(0f, 1f, 4f) : new Vector3(-4f, 1f, -4f);
                    break;
                case < 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(0f, 1f, 4f) : new Vector3(4f, 1f, -4f);
                    break;
                case >= 0f when transform.position.x >= 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(0f, 1f, -4f) : new Vector3(-4f, 1f, 4f);
                    break;
                case >= 0f:
                    targetPosition = Random.value > 0.5 ? new Vector3(0f, 1f, -4f) : new Vector3(4f, 1f, 4f);
                    break;
            }
        }
        
        StartCoroutine(DestroyObject());
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyDelay);
        
        Destroy(this.gameObject);
    }
}
