using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBombInactive : MonoBehaviour
{
    public float timer;

    public GameObject explosion;

    public LayerMask hitLayers;

    public float hitDistance;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            ExplodeBomb();
        }
    }

    private void ExplodeBomb()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
