// <copyright file="Asteroid.cs" company="DIS Copenhagen">
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
	public float speed = 1;
    [Tooltip("How long does the asteroid life before it is automatically destroyed, in seconds")]
    public float lifeTime = 10;
    [Tooltip("How fast does the asteroid rotate, in degrees")]
    public float rotationSpeed = 60;
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public GameObject explosionPrefab;

	Vector2 direction = new Vector2();

    /// <summary>
    /// Start this instance. Get Called by Unity when this GameObject enters the scene
    /// </summary>
    void Start(){
		direction = new Vector2(0,-1);
		// normalize direction so it does not impact the travel speed
		direction.Normalize ();
    }

    /// <summary>
    /// Moves the asteroid downwards using speed and rotates it using rotationSpeed.
	/// Also kills the object after lifeTime expires. Update is called each frame by Unity.
    /// </summary>
	void Update () {
		transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward);
		
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
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
