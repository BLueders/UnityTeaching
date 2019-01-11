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
	public float minSpeed = 1;
	[Tooltip("How fast does the asteroid move in units per second")]
	public float maxSpeed = 5;
	[Tooltip("Direction of the asteroids movement")]
	public float maxXAngle = -1;
	[Tooltip("Direction of the asteroids movement")]
	public float minXAngle = 1;
    [Tooltip("How long does the asteroid life before it is automatically destroyed, in seconds")]
    public float lifeTime = 1;
    [Tooltip("How fast does the asteroid rotate, in degrees")]
    public float rotationSpeed = 60;
    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public GameObject explosionPrefab;
	[Tooltip("Maximum Hitpoints of the asteroid")]
	public int maxHP = 1;

	// powerups
	[Header("Powerups")]
	[Tooltip("Chance of dropping a powerup")]
	public float puDropchance = 0.1f;
	[Tooltip("List of droppable powerups")]
	public Powerup[] powerups;

	float speed;
	int hp;
	Vector2 direction = new Vector2();

    /// <summary>
    /// Start this instance. Get Called by Unity when this GameObject enters the scene
    /// </summary>
    void Start(){
		hp = maxHP;
		speed = Random.Range (minSpeed, maxSpeed);
		direction.x = Random.Range (minXAngle, maxXAngle);
		direction.y = -1;
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
	/// Evaluate a hit on the asteroid. Is called by another instance when it collides with the asteroid.
	/// </summary>
	public void OnHit(){ 
		hp--;
		if (hp <= 0) {
			Die ();
		}
	}

    /// <summary>
    /// Destroys the asteroid.
    /// </summary>
    public void Die(){ 

		float puDrop = Random.value;
		if (puDrop < puDropchance) {
			Powerup powerup = powerups [Random.Range (0, powerups.Length)];
			Instantiate(powerup.gameObject, transform.position, Quaternion.identity);
		}

		// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
