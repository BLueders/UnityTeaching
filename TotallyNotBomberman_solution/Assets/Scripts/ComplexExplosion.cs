using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexExplosion : MonoBehaviour
{
    public GameObject horizontalMid;
    public GameObject verticalMid;
    public GameObject endUp;
    public GameObject endDown;
    public GameObject endLeft;
    public GameObject endRight;
    public float timer;

    List<GameObject> explosionParts;

    [HideInInspector]
    public float distUp;
    [HideInInspector]
    public float distDown;
    [HideInInspector]
    public float distLeft;
    [HideInInspector]
    public float distRight;
    [HideInInspector]
    public float distMax;

    public void Init()
    {
        explosionParts = new List<GameObject>();

        SpawnExplosion(distUp, Vector3.up, verticalMid, endUp);
        SpawnExplosion(distDown, Vector3.down, verticalMid, endDown);
        SpawnExplosion(distLeft, Vector3.left, horizontalMid, endLeft);
        SpawnExplosion(distRight, Vector3.right, horizontalMid, endRight);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            foreach(GameObject obj in explosionParts)
            {
                Destroy(obj);
            }
            Destroy(gameObject);
        }
    }

    void SpawnExplosion(float dist, Vector3 dir, GameObject midPart, GameObject endPart) {
        float spawnOffset = 1;
        while (spawnOffset <= dist && spawnOffset < distMax)
        {
            Vector3 pos = transform.position;
            pos += dir*spawnOffset;
            GameObject newPart = Instantiate<GameObject>(midPart, pos, Quaternion.identity);
            explosionParts.Add(newPart);
            spawnOffset += 1;
        }
        if (dist == distMax)
        {
            Vector3 pos = transform.position;
            pos += dir * spawnOffset;
            GameObject newPart = Instantiate<GameObject>(endPart, pos, Quaternion.identity);
            explosionParts.Add(newPart);
        }
    }
}
