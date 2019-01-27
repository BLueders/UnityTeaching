using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer sr;

    public Sprite up;
    public Sprite down;
    public Sprite side;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 vel = rb2D.velocity;
        if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
        {
            if (vel.y > 0)
            {
                sr.sprite = up;
            }
            if (vel.y < 0)
            {
                sr.sprite = down;
            }
        }
        else if(Mathf.Abs(vel.x) != 0)
        {
            sr.sprite = side;
            if (vel.x > 0)
            {
                sr.flipX = false;
            }
            if (vel.x < 0)
            {
                sr.flipX = true;
            }
        }
    }
}
