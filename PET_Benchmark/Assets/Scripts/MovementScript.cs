using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovementScript
{
    public bool IsCollisionObject { get; set; }
    public bool CollisionHappened { get; set; }
    public Vector3 SpawnPosition { get; set; }
    public Vector3 TargetPosition { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
