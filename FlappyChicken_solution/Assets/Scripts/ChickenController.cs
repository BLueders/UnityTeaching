/*
 * Created on Mon Jun 18 2018
 *
 * Copyright (c) 2018 DIS
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A basic controller for the flappy chicken
/// </summary>
public class ChickenController : MonoBehaviour
{
    public float speed;
    public float flapForce;

    public Sprite upSprite;
    public Sprite downSprite;

    public AudioClip[] chickenSounds;

    private Rigidbody2D myRb2D;
    private SpriteRenderer mySpriteRenderer;

    private AudioSource myAudioSource;

    /// <summary>
    /// Called when the game starts, grabs our required components to work with later in the game
    /// </summary>
    void Start()
    {
        myRb2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called every frame and handels the movement of the chicken
    /// </summary>
    void Update()
    {
        // grab our velocity to work with later.
        Vector2 vel = myRb2D.velocity;
        // set the x (horizontal) velocity to be our speed
        vel.x = speed;

        // use correct sprite when moving up or down (flap)
        if (vel.y < 0)
        {
            mySpriteRenderer.sprite = downSprite;
        }
        else
        {
            mySpriteRenderer.sprite = upSprite;
        }

        // flap when pressing space (set y, vertical, velocity to be flapForce)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vel.y = flapForce;
            myRb2D.velocity = vel;
            myAudioSource.PlayOneShot(chickenSounds[Random.Range(0, chickenSounds.Length)]);
        }

        // make the chicken look into the direction it is flying (up or down)
        vel.Normalize();
        // Get rotation angle using Atan2
        float angle = Mathf.Atan2(vel.y, vel.x);
        // Create rotation around forward axis, sprite is facing upwards per default, so substract 90 degrees (Atan2 angle 0 is in x direction or right)
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg / 2, Vector3.forward);

    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="col">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
