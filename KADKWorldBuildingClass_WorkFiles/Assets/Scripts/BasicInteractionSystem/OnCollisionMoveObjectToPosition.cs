using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionMoveObjectToPosition : CollisionAction
{
    public GameObject objectToMove;
    public Vector3 targetPosition;
    public float transitionTime = 0;

    private Coroutine routine;

    protected override void DoAction()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(Move());
    }

    protected override void Start()
    {
        base.Start();
        if (objectToMove == null)
        {
            Debug.LogError("OnCollisionMoveObjectToPosition on " + name + " requires object assigned");
        }
    }

    IEnumerator Move()
    {
        float startTime = Time.time;
        while (startTime + transitionTime < Time.time)
        {
            float normalizedTime = (Time.time - startTime) / transitionTime;
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, normalizedTime);
            yield return null;
        }
        objectToMove.transform.position = targetPosition;
    }
}
