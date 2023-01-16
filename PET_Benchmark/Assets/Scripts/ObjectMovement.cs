using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay;
    private Vector3 targetPosition;
    
    void Start()
    {
        targetPosition = new Vector3(transform.position.x * (-1), 1f, transform.position.z * (-1));
        StartCoroutine(DestroyObject());
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyDelay);
        
        Destroy(this.gameObject);
    }
}
