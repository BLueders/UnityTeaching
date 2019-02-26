// <copyright file="SpaceShip.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// This script handles the spaceships functionality, including movement and shooting. The spaceship needs any Collider2D attached that has the trigger flag checked, so it can be hit
/// by the asteroids and die.
/// </summary>
public class SpaceShip : MonoBehaviour
{

	public enum FireMode
	{
		Normal,
		ThreeShot
	}

	[Tooltip ("How fast is the spaceship moving left to right")]
	public float speed;
	[Tooltip ("How fast is the spaceship shooting")]
	public float rateOfFire;
	[Tooltip ("Prefab to be instantiated when shooting (Projectile)")]
	public GameObject projectilePrefab;
	[Tooltip ("Prefab to be instantiated when dying (explosioin)")]
	public GameObject explosionPrefab;
	[Tooltip ("An Audioclip that is played when the ship shoots")]
	public AudioClip laserSound;
	[Tooltip ("Maximum HP of the spaceship. Each Asteroid deals 1 damage.")]
	public int maxHP = 4;
	[Tooltip ("How long ins the player invulnerable when hurt.")]
	public float hurtTimer = 1;

	[Tooltip ("Firemode to use")]
	public FireMode fireMode = FireMode.Normal;

	private float lastTimeFired = 0;
	private int hp;
	private bool ishurting = false;
	private List<ActiveEffect> effectList = new List<ActiveEffect> ();

	void Start ()
	{
		hp = maxHP;
		UIHealthPanel.instance.SetLives (maxHP, hp);
	}

	/// <summary>
	/// This is called by Unity every frame. It handles the ships movement and checks if it should fire
	/// </summary>
	void Update ()
	{

		// move the ship left and right, depending on the horizontal input
		transform.position += Vector3.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

		UpdateShooting ();
	}

	/// <summary>
	/// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D collider attached, at least one of them is a trigger and at least of of
	/// the two colliding GameObjects has a Rigidbody2D attached.
	/// </summary>
	void OnTriggerEnter2D (Collider2D other)
	{

		// if the other object has the asteroid tag, the destroy the ship and restard the game
		if (other.CompareTag ("Asteroid")) {
			other.GetComponent<Asteroid> ().Die ();
			Hurt ();
		}

		if (other.CompareTag ("Powerup")) {
			ActivatePowerup (other.GetComponent<Powerup> ());
			Destroy (other.gameObject);
		}
	}

	/// <summary>
	/// Hurt the Player. The player will lose one hitpoint and is invulnerable for hurtTimer time.
	/// </summary>
	void Hurt ()
	{
		if (ishurting) {
			return;
		}
		ishurting = true;
		hp--;
		UIHealthPanel.instance.SetLives (maxHP, hp);
		if (hp <= 0) {
			Die ();
		}
		StartCoroutine (HurtRoutine ());
	}

	IEnumerator HurtRoutine ()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		float startTime = Time.time;
		while (startTime + hurtTimer > Time.time) {
            sr.color = Color.red;
			yield return new WaitForSeconds (0.05f);
			sr.color = Color.white;
			yield return new WaitForSeconds (0.05f);
		}
		sr.color = Color.white;
		ishurting = false;
	}

	void Die ()
	{
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		// kill the spaceship (not instantly, Destroy() will remove the Gameobject from the scene after this Updatecycle)
		Destroy (gameObject);
		// load the active scene again, to restard the game. The GameManager will handle this for us. We use a slight delay to see the explosion.
		GameManager.instance.RestartTheGameAfterSeconds (1);
	}

	/// <summary>
	/// Helper function to include the shooting behavior.
	/// </summary>
	void UpdateShooting ()
	{
		// if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
		if (Input.GetButton ("Fire") && (lastTimeFired + 1 / rateOfFire) < Time.time) {
			switch (fireMode) {
			case FireMode.Normal:
				FireNormalLaser ();
				break;
			case FireMode.ThreeShot:
				FireThreeShotLaser ();
				break;
			}
			lastTimeFired = Time.time;
		}
	}

	/// <summary>
	/// This functions shoots just one laser to the front
	/// </summary>
	void FireNormalLaser ()
	{
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
		Instantiate (projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
	}

	/// <summary>
	/// This function shoots three lasers at once in different angles
	/// </summary>
	void FireThreeShotLaser ()
	{
		AudioSource.PlayClipAtPoint (laserSound, transform.position);

		// Shooting up
		Instantiate (projectilePrefab, transform.position + Vector3.up, Quaternion.identity);

		// Shooting left
		GameObject projectileObject = Instantiate (projectilePrefab, transform.position + Vector3.up + Vector3.left, Quaternion.identity) as GameObject;
		Projectile projectile = projectileObject.GetComponent<Projectile> ();
		projectile.direction.x = -0.5f;

		// Shooting right
		projectileObject = Instantiate (projectilePrefab, transform.position + Vector3.up + Vector3.right, Quaternion.identity) as GameObject;
		projectile = projectileObject.GetComponent<Projectile> ();
		projectile.direction.x = 0.5f;
	}

	void ActivatePowerup (Powerup powerup)
	{
		if (powerup.unique) {
			ActiveEffect effectToRemove = null;
			foreach (ActiveEffect activeEffect in effectList) {
				if (activeEffect.effect.type == powerup.effect.type) {
					StopCoroutine (activeEffect.controllingRoutine);
					activeEffect.effect.ResetEffect (this);
					effectToRemove = activeEffect;
				}
			}
			if (effectToRemove != null) {
				effectList.Remove (effectToRemove);
			}
		}
		ActiveEffect newEffect = new ActiveEffect ();
		newEffect.effect = powerup.effect;
		Coroutine newControllingRoutine = StartCoroutine (powerupRoutine (newEffect, powerup.effectDuration));
		newEffect.controllingRoutine = newControllingRoutine;
		effectList.Add (newEffect);
	}

	IEnumerator powerupRoutine (ActiveEffect activeEffect, float time)
	{
		activeEffect.effect.ApplyEffect (this);
		yield return new WaitForSeconds (time);
		activeEffect.effect.ResetEffect (this);
		effectList.Remove (activeEffect);
	}

	public class ActiveEffect
	{
		public PowerupEffect effect;
		public Coroutine controllingRoutine;
	}
}
