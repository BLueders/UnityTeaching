using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionSpawnObject : CollisionAction
{
    public GameObject prefabToSpawn;
    public Transform target;
    public bool respectTargetRotation = false;

    protected override void DoAction()
    {
        if (respectTargetRotation)
        {
            Instantiate(prefabToSpawn, target.position, target.rotation);
        } else
        {
            Instantiate(prefabToSpawn, target.position, Quaternion.identity);
        }
    }

    protected override void Start()
    {
        base.Start();
        if (prefabToSpawn == null)
        {
            Debug.LogError("OnCollisionSpawnObject on object " + name + " requires Prefab To Spawn assigned");
        }
        if (target == null)
        {
            Debug.LogError("OnCollisionSpawnObject on object " + name + " requires target assigned");
        }
    }
}
