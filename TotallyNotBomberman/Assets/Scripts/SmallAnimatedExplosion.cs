using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAnimatedExplosion : MonoBehaviour
{
    SpriteRenderer sr;

    public Sprite[] frames;
    public float animationFPS;

    float frameTimer;
    int currentFrame;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameTimer = 0;
        currentFrame = 0;
    }

    void Update()
    {
        frameTimer -= Time.deltaTime;
        if(frameTimer <= 0)
        {
            sr.sprite = frames[currentFrame];
            frameTimer = 1 / animationFPS;
            currentFrame++;
            if(currentFrame >= frames.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}
