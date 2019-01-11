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
		StartCoroutine (KillAfterSeconds (lifeTime));
		// normalize direction so it does not impact the travel speed
		direction.Normalize ();
		// make the projectile rotate into the direction it is moving, math will be addressed in lecture 2
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	/// <summary>
	/// Update is called by Unity each frame. This moves the GameObject upwards at speed.
	/// </summary>
	void Update ()
	{
		transform.position += new Vector3 (direction.x, direction.y, 0) * speed * Time.deltaTime;
	}

	/// <summary>
	/// This is called by Unity when the GameObject where this script is on collides with another GameObject. 
	/// Both objects need to have 2D colliders attached, at least one of them needs to have the collider set to trigger and at least one of them needs to have a Rigidbody2D attached!!
	/// On collision the projectile is destroyed and if it hits an object with the tag "Asteroid" the asteroid will be notified it got hit.
	/// </summary>
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Asteroid")) { // This checks if we hit an asteroid. The asteroid needs the "Asteroid" tag for this to work!!
			Asteroid asteroid = other.GetComponent<Asteroid> (); // Grab the asteroid script from the hit GameObject
			asteroid.OnHit (); // notify the asteroid it got hit
			Destroy (gameObject); // Destory this projectile
		}
	}

	/// <summary>
	/// Destroys the projectile after seconds. This is a coroutine that needs be started using StartCoroutine().
	/// </summary>
	IEnumerator KillAfterSeconds (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
