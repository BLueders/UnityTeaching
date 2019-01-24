// <copyright file="Explosion.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the explosion animation on the explosion GameObject. 
/// It requires a SpriteRenderer, as the SpriteRenderer is used in the script to cycle through the sprites of the animation.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Explosion : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond = 5;
    [Tooltip("An Audioclip with the sound that is played when the explosion happens")]
    public AudioClip explosionSound;

    SpriteRenderer spriteRenderer;
    int currentFrameIndex = 0;
    float frameTimer;

    /// <summary>
    /// Start is called by Unity. This will play our explosion sound and start the sprite animation
    /// </summary>
    void Start()
    {
        if (explosionSound != null) {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameTimer = (1f / framesPerSecond);
        currentFrameIndex = 0;
    }

    /// <summary>
    /// Cycles through the sprites of our explosion animation.
    /// </summary>
    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer <= 0) {
            currentFrameIndex++;
            if (currentFrameIndex >= frames.Length) {
                Destroy(gameObject);
                return;
            }
            frameTimer = (1f / framesPerSecond);
            spriteRenderer.sprite = frames[currentFrameIndex];
        }
    }
}
