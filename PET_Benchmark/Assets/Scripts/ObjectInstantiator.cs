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
    [SerializeField] [Tooltip("indicate the order of patterns 1, 2 and 3")] private List<int> patternOrder;

    private List<Vector3> positions;
    private Dictionary<int, Vector3> passObjDictionary;

    [Header("Pattern 1")]
    [SerializeField] [Tooltip("y coordinate should always be 1; coordinates will be used to calculate the corresponding point on a circle around the player with radius 4")]  private List<Vector3> objPositions1;
    [SerializeField] [Tooltip("note: dictionaries cannot be serialized yet; add indices of objects that should not collide with the player but pass them")]  private List<int> passIndices1;
    [SerializeField] [Tooltip("add coordinates of the general target position, the corresponding point on the circle will be calculated")]  private List<Vector3> passTargetPos1;
    private Dictionary<int, Vector3> passObj1 = new Dictionary<int, Vector3>();
    
    [Header("Pattern 2")]
    [SerializeField] [Tooltip("y coordinate should always be 1; coordinates will be used to calculate the corresponding point on a circle around the player with radius 4")]  private List<Vector3> objPositions2;
    [SerializeField] [Tooltip("note: dictionaries cannot be serialized yet; add indices of objects that should not collide with the player but pass them")]  private List<int> passIndices2;
    [SerializeField] [Tooltip("add coordinates of the general target position, the corresponding point on the circle will be calculated")]  private List<Vector3> passTargetPos2;
    private Dictionary<int, Vector3> passObj2 = new Dictionary<int, Vector3>();
    
    [Header("Pattern 3")]
    [SerializeField] [Tooltip("y coordinate should always be 1; coordinates will be used to calculate the corresponding point on a circle around the player with radius 4")]  private List<Vector3> objPositions3;
    [SerializeField] [Tooltip("note: dictionaries cannot be serialized yet; add indices of objects that should not collide with the player but pass them")]  private List<int> passIndices3;
    [SerializeField] [Tooltip("add coordinates of the general target position, the corresponding point on the circle will be calculated")]  private List<Vector3> passTargetPos3;
    private Dictionary<int, Vector3> passObj3 = new Dictionary<int, Vector3>();
    
    private float xObj, zObj, mLine;

    public GameObject instantiatedObj; // specific object that was instantiated

    private GameObject player;

    private void Start()
    {
        CreatePassObjDictionary(passIndices1, passTargetPos1, passObj1);
        CreatePassObjDictionary(passIndices2, passTargetPos2, passObj2);
        CreatePassObjDictionary(passIndices3, passTargetPos3, passObj3);
        StartCoroutine(SpawnObjects());
        
        player = GameObject.FindWithTag("Player");
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(preparationDelay);

        // for each pattern (3)
        for (int c = 0; c < 3; c++)
        {
            CheckWhichPatternShouldPlayAndSetVariables(c);
            
            // for each entry in the list of general positions set in the editor
            for (var i = 0; i < positions.Count; i++)
            {
                Debug.Log(player.transform.position);
                // wait until participant returns to center; TODO: does not seem to work suddenly
                yield return new WaitUntil(() =>
                    player.transform.position == new Vector3(0, 1.1f, 0));
                
                // wait before instantiating the next object
                yield return new WaitForSeconds(delay);
                
                // reset playerMovementCounter for each object
                player.GetComponent<GridMovement>().PlayerMovementCounter = 0;
                
                var dirPos = positions[i];
                // calculate the corresponding point on the circle around the participant
                Vector3 pos = CalculateCoordinates(dirPos);
                Debug.Log(pos);
            
                // instantiate object
                instantiatedObj = Instantiate(obj, pos, Quaternion.identity, this.transform);
                Debug.Log(("inst"));
            
                // check if object should just pass the participant
                foreach (var (ind, targetPos) in passObjDictionary)
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
            }

            Debug.Log("next pattern will be played");
            yield return new WaitForSeconds(preparationDelay * 2f);
        }
        GetComponent<WriteToCSV>().Write(GameObject.Find("ScoreManager").GetComponent<ScoreManager>().IndividualScores, patternOrder);
        
        // show total score at the end of the experiment (after each of the 3 patterns were played)
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Invoke("ShowTotalScore", 0f);
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
    private void CreatePassObjDictionary(List<int> indices, List<Vector3> targets, Dictionary<int, Vector3> dic)
    {
        for (var i = 0; i < indices.Count; i++)
        {
            dic[indices[i]] = targets[i];
        }
    }

    // checks which pattern should be played according to the pattern order set in the editor
    private void CheckWhichPatternShouldPlayAndSetVariables(int index)
    {
        switch (patternOrder[index])
        {
            case 1:
                positions = objPositions1;
                passObjDictionary = passObj1;
                break;
            case 2:
                positions = objPositions2;
                passObjDictionary = passObj2;
                break;
            case 3:
                positions = objPositions3;
                passObjDictionary = passObj3;
                break;
        }
    }
}
