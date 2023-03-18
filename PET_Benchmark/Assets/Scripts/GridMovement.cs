using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public GameObject[] grid = new GameObject[9];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (Input.GetKeyDown("[" + (i+1) + "]"))
            {
                this.transform.position = grid[i].transform.position + new Vector3(0,1.1f,0);
                
            }
        }
        
        
    }
}
