using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionMoveObjectToTarget : CollisionAction
{
    public GameObject objectToMove;
    public GameObject targetObject;
    public float transitionTime = 0;

    private Coroutine routine;

    protected override void DoAction()
    {
        if (routine!=null)
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
            Debug.LogError("OnCollisionMoveObjectToTarget on " + name + " requires object assigned");
        }
        if (targetObject == null)
        {
            Debug.LogError("OnCollisionMoveObjectToTarget on " + name + " requires target assigned");
        }
    }

    IEnumerator Move()
    {
        float startTime = Time.time;
        while (startTime + transitionTime < Time.time)
        {
            float normalizedTime = (Time.time - startTime) / transitionTime;
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetObject.transform.position, normalizedTime);
            yield return null;
        }
        objectToMove.transform.position = targetObject.transform.position;
    }
}