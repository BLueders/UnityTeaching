using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveSpace
{
    Global,
    Local
}

public class OnCollisionMoveObject : CollisionAction
{
    public GameObject objectToMove;
    public Vector3 distanceToMove;
    public float transitionTime = 0;
    public MoveSpace space = MoveSpace.Global;

    private Coroutine routine;
    private Vector3 offset;

    protected override void DoAction()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        offset = distanceToMove;
        if (space == MoveSpace.Local)
        {
            offset = transform.rotation * distanceToMove;
        }
        routine = StartCoroutine(Move());
    }

    protected override void Start()
    {
        base.Start();
        if (objectToMove == null)
        {
            Debug.LogError("OnCollisionMoveObject on " + name + " requires object assigned");
        }
    }

    IEnumerator Move()
    {
        float startTime = Time.time;
        Vector3 targetPosition = transform.position + offset;
        while (startTime + transitionTime < Time.time)
        {
            float normalizedTime = (Time.time - startTime) / transitionTime;
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, normalizedTime);
            yield return null;
        }
        objectToMove.transform.position = targetPosition;
    }
}
