using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDestroyObject : CollisionAction
{
    public GameObject objectToDestroy;

    protected override void DoAction()
    {
        Destroy(objectToDestroy);
    }

    protected override void Start()
    {
        base.Start();
        if(objectToDestroy == null)
        {
            Debug.LogError("OnCollisionDestroyObject on " + name + " requires object to destroy assigned");
        }
    }
}
