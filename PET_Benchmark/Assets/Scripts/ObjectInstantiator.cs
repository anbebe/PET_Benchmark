using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Instantiates objects the participant needs to avoid colliding with on a circle with radius 4 around the participant
/// </summary>
public class ObjectInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject obj; // prefab
    [SerializeField] private float delay;
    [SerializeField] private float preparationDelay;
    
    [SerializeField] [Tooltip("y coordinate should always be 1; coordinates will be used to calculate the corresponding point on a circle around the player with radius 4")]  private List<Vector3> objPositions;

    [SerializeField] [Tooltip("note: dictionaries cannot be serialized yet; add indices of objects that should not collide with the player but pass them")]  private List<int> passIndices;
    [SerializeField] [Tooltip("add coordinates of the general target position, the corresponding point on the circle will be calculated")]  private List<Vector3> passTargetPos;
    private Dictionary<int, Vector3> passObj = new Dictionary<int, Vector3>();
    
    private float xObj, zObj, mLine;

    private GameObject instantiatedObj; // specific object that was instantiated

    private void Start()
    {
        CreatePassObjDictionary(passIndices, passTargetPos);
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(preparationDelay);

        // for each entry in the list of general positions set in the editor
        for (var i = 0; i < objPositions.Count; i++)
        {
            var dirPos = objPositions[i];
            // calculate the corresponding point on the circle around the participant
            Vector3 pos = CalculateCoordinates(dirPos);
            
            // instantiate object
            instantiatedObj = Instantiate(obj, pos, Quaternion.identity, this.transform);
            
            // check if object should just pass the participant
            foreach (var (ind, targetPos) in passObj)
            {
                if (ind == i) 
                {
                    // if yes, set collision bool to false and target position (calculated to be on the circle) as set in the editor
                    instantiatedObj.GetComponent<ObjectMovement>().IsCollisionObject = false;
                    instantiatedObj.GetComponent<ObjectMovement>().TargetPosition = CalculateCoordinates(targetPos);
                    break;
                }
                // if not, set collision bool to true
                instantiatedObj.GetComponent<ObjectMovement>().IsCollisionObject = true;
                
            }
            
            // wait before instantiating the next object
            yield return new WaitForSeconds(delay);
        }
    }

    // takes the coordinates of the general point (direction) and calculates the coordinates of the corresponding point on the circle
    // (calculates the intersection of the circle and a line that goes through the center (0,0,0) and the general point set in the editor)
    private Vector3 CalculateCoordinates(Vector3 dirPos)
    {
        mLine = dirPos.z / dirPos.x;
        xObj = Mathf.Sqrt(16 / (Mathf.Pow(mLine, 2f) + 1));
        zObj = Mathf.Sqrt(16 - Mathf.Pow(xObj, 2));

        // if coordinates set in the editor were negative, set the calculated to be negative as well
        // (Mathf.Sqrt gives positive value of the square root as the result)
        if (dirPos.x < 0)
        {
            xObj *= (-1);
        }

        if (dirPos.z < 0)
        {
            zObj *= (-1);
        }
        
        return new Vector3(xObj, 1f, zObj);
    }

    // Unity cannot handle serialized dictionaries yet, so we need to work with 2 lists in the editor and create a dictionary from these entries
    private void CreatePassObjDictionary(List<int> indices, List<Vector3> targets)
    {
        for (var i = 0; i < indices.Count; i++)
        {
            passObj[indices[i]] = targets[i];
        }
    }
}
