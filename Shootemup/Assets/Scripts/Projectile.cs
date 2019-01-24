// <copyright file="Projectile.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile handles the movement of the fired lasers. The script needs any 2D Collider (set to trigger) and a Rigidbody2D (set to kinematic) on the same GameObject.
/// The script moves the GameObject constantly upwards using speed. After the lifeTime the projectile is
/// </summary>
public class Projectile : MonoBehaviour
{

	[Tooltip ("How fast is the projectile moving upwards")]
	public float speed = 2;
	[Tooltip ("After how many seconds is the projectile destroyed")]
	public float lifeTime = 3;
	[Tooltip ("The direction the laster travels")]
	public Vector2 direction = new Vector2 (0, 1);

	/// <summary>
	/// This is called by Unity. It starts the coroutine that destroyes the projectile after the lifetime.
	/// </summary>
	void Start ()
	{
		// normalize direction so it does not impact the travel speed
		direction.Normalize ();
		// make the projectile rotate into the direction it is moving, math will be addressed in lecture 2
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	/// <summary>
	/// Update is called by Unity each frame. This moves the GameObject upwards at speed and kills the object after lifeTime expires
	/// </summary>
	void Update ()
	{
		transform.position += new Vector3 (direction.x, direction.y, 0) * speed * Time.deltaTime;
		
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D Collider attached, none them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached. If one of the two 2D Colliders is a trigger, OnTriggerEnter2D(Collider other) is called instead. </summary>
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Asteroid")) { // This checks if we hit an asteroid. The asteroid needs the "Asteroid" tag for this to work!!
			Asteroid asteroid = col.gameObject.GetComponent<Asteroid> (); // Grab the asteroid script from the hit GameObject
			asteroid.OnHit (); // notify the asteroid it got hit
			Destroy (gameObject); // Destory this projectile
		}
	}
}
