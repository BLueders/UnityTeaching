using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBomb : MonoBehaviour
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
        DoRaycast(Vector2.up);
        DoRaycast(Vector2.down);
        DoRaycast(Vector2.left);
        DoRaycast(Vector2.right);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void DoRaycast(Vector2 dir) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, hitDistance, hitLayers);

        if (hit.collider != null)
        {
            PlayerHealth ph = hit.collider.gameObject.GetComponent<PlayerHealth>();
            if (ph != null) {
                ph.Hit();
            }

            Box box = hit.collider.gameObject.GetComponent<Box>();
            if (box != null) {
                box.Hit();
            }
        }
    }
}
