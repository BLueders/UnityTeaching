﻿// <copyright file="Asteroid.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// This script needs to be attached to the games asteroid objects. A asteroid also needs the asteroid sprite, any 2D collider (set to trigger) and a Rigidbody2D (set to kinematic) attached to its GameObject.
/// </summary>
public class Asteroid : MonoBehaviour {

    [Tooltip("How fast does the asteroid move in units per second")]
	public float speed = 20;
	[Tooltip("Direction of the asteroids movement")]
	public Vector2 direction = new Vector2(0, -1);
    [Tooltip("How long does the asteroid life before it is automatically destroyed, in seconds")]
    public float lifeTime = 1;
    [Tooltip("How fast does the asteroid rotate, in degrees")]
    public float rotationSpeed = 60;
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public GameObject explosionPrefab;

    /// <summary>
    /// Start this instance. Get Called by Unity when this GameObject enters the scene
    /// </summary>
    void Start(){
        // Start the KillAfterSeconds coroutine immediately, so the asteroid is destroyed after lifetime seconds pass.
        StartCoroutine(KillAfterSeconds(lifeTime));
		// normalize direction so it does not impact the travel speed
		direction.Normalize ();
    }

    /// <summary>
    /// Moves the asteroid downwards using speed and rotates it using rotationSpeed. Update is called each frame by Unity
    /// </summary>
	void Update () {
		transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward);
	}

    /// <summary>
    /// Destroys the asteroid after seconds. This is a coroutine that needs be started using StartCoroutine().
    /// </summary>
    IEnumerator KillAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds); // this halts the functions execution for x seconds. Can only be used in coroutines.
        Destroy(gameObject);
    }

    /// <summary>
    /// Function to evaluate a hit on the asteroid. Is called by another instance when it collides with the asteroid.
    /// </summary>
    public void OnHit(){ 
		// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
