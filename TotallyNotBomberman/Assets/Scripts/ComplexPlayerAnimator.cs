using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexPlayerAnimator : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer sr;

    public float animationFPS;

    public Sprite upStanding;
    public Sprite downStanding;
    public Sprite sideStanding;

    public Sprite[] upMoving;
    public Sprite[] downMoving;
    public Sprite[] sideMoving;

    Sprite[] spriteAnimation;
    int currentFrame;
    float animationTimer;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentFrame = 0;
    }

    void Update()
    {
        Vector3 vel = rb2D.velocity;

        if(vel.magnitude == 0)
        {
            if(spriteAnimation == upMoving)
            {
                sr.sprite = upStanding;
            }
            if (spriteAnimation == downMoving)
            {
                sr.sprite = downStanding;
            }
            if (spriteAnimation == sideMoving)
            {
                sr.sprite = sideStanding;
            }
        }
        else
        {
            if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
            {
                if (vel.y > 0)
                {
                    spriteAnimation = upMoving;
                }
                if (vel.y < 0)
                {
                    spriteAnimation = downMoving;
                }
            }
            else
            {
                spriteAnimation = sideMoving;
                if (vel.x > 0)
                {
                    sr.flipX = false;
                }
                if (vel.x < 0)
                {
                    sr.flipX = true;
                }
            }

            animationTimer -= Time.deltaTime;
            if(animationTimer <= 0)
            {
                animationTimer = 1f / animationFPS;
                currentFrame++;
                if (currentFrame >= spriteAnimation.Length) {
                    currentFrame = 0;
                }
                sr.sprite = spriteAnimation[currentFrame];
            }
        }
    }
}
