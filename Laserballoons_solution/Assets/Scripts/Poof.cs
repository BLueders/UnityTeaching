// <copyright file="Poof.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

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

    /// <summary>
    /// Start is called by Unity. This will play our poof sound and start the sprite animation
    /// </summary>
    void Start()
    {
		if (poofSound != null) {
			AudioSource.PlayClipAtPoint(poofSound, transform.position);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    /// <summary>
    /// This is a coroutine that cycles through the sprites of our poof animation. It needs to be started using StartCoroutine().
    /// </summary>
    IEnumerator PlayAnimation()
    {
        int currentFrameIndex = 0;
        while (currentFrameIndex < frames.Length) {
            spriteRenderer.sprite = frames [currentFrameIndex];
            yield return new WaitForSeconds(1f / framesPerSecond); // this halts the functions execution for x seconds. Can only be used in coroutines.
            currentFrameIndex++;
        }
        Destroy(gameObject);
    }
}
