using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float delay;
    [SerializeField] private float preparationDelay;
    private bool spawnObjects;

    public bool SpawnNewObjects
    {
        get => spawnObjects;
        set => spawnObjects = value;
    }
    
    void Start()
    {
        spawnObjects = true;
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(preparationDelay);
        
        while (spawnObjects)
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized * 4f;
            Vector3 pos = new Vector3(randomPoint.x, 1f, randomPoint.y);
            Instantiate(obj, pos, Quaternion.identity, this.transform);

            yield return new WaitForSeconds(delay);
        }
    }
}
