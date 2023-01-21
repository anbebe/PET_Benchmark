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
    public Vector3 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }
    
    private bool isCollisionObject;
    public bool IsCollisionObject
    {
        set => isCollisionObject = value;
    }

    private void Start()
    {
        if (isCollisionObject)
        {
            Debug.Log("obj is collider object");
            targetPosition = new Vector3(transform.position.x * (-1), 1f, transform.position.z * (-1));
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
