// <copyright file="Poof.cs" company="DIS Copenhagen">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2019</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the balloon popping animation on the destroyed Balloon. 
/// It requires a SpriteRenderer, as the SpriteRenderer is used in the script to cycle through the sprites of the animation.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Poof : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond;
    [Tooltip("An Audioclip with the sound that is played when the poof happens")]
    public AudioClip poofSound;
    SpriteRenderer spriteRenderer;

    // A simple timer to wait for 1 second to destroy the object
    private float destructionTimer = 1;

    /// <summary>
    /// Start is called by Unity. This will play our poof sound and start the destruction timer.
    /// </summary>
    void Start()
    {
		if (poofSound != null) {
			AudioSource.PlayClipAtPoint(poofSound, transform.position);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    /// <summary>
    /// Currently the Update method only waits for one second [destructionTimer] and then removes the gameObject from the scene.
    /// Modify this to play back the sprite animation using [frames] and [frameTimer].
    /// </summary>
    void Update()
    {
        // decrease Timer value
        destructionTimer -= Time.deltaTime;
        if(destructionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
