using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingBomb : MonoBehaviour
{
    public GameObject explosion;

    public LayerMask hitLayers;

    public float hitDistance;

    SpriteRenderer sr;
    Collider2D col;

    public Sprite[] frames;
    public float timer;

    float bombTimer;
    float frameTimer;
    int currentFrame;

    void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        sr = GetComponent<SpriteRenderer>();
        frameTimer = 0;
        currentFrame = 0;
        bombTimer = timer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ExplodeBomb();
        }

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0 && timer >= 0)
        {
            sr.sprite = frames[currentFrame];
            frameTimer = bombTimer / frames.Length;
            currentFrame++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            col.isTrigger = false;
        }
    }

    private void ExplodeBomb()
    {
        GameObject expObj = Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);

        DoRaycast(Vector2.up);
        DoRaycast(Vector2.down);
        DoRaycast(Vector2.left);
        DoRaycast(Vector2.right);

        Destroy(gameObject);
    }

    private float DoRaycast(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, hitDistance, hitLayers);

        if (hit.collider != null)
        {
            PlayerHealth ph = hit.collider.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.Hit();
            }

            Box box = hit.collider.gameObject.GetComponent<Box>();
            if (box != null)
            {
                box.Hit();
            }
            return hit.distance;
        }

        return hitDistance;
    }

}
