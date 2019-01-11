// <copyright file="SpaceShip.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>18/06/2018</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the spaceships functionality, including movement and shooting. The spaceship needs any Collider2D attached that has the trigger flag checked, so it can be hit
/// by the asteroids and die.
/// </summary>
public class SpaceShip : MonoBehaviour {

	[Tooltip("How fast is the spaceship moving left to right")]
    public float speed = 1;
	[Tooltip("How fast is the spaceship shooting")]
    public float rateOfFire = 1;
    [Tooltip("Prefab to be instantiated when shooting (Projectile)")]
    public GameObject projectilePrefab;
    [Tooltip("Prefab to be instantiated when dying (explosioin)")]
    public GameObject explosionPrefab;
    [Tooltip("An Audioclip that is played when the ship shoots")]
    public AudioClip laserSound;

    private float lastTimeFired = 0;
	private bool isDead = false;

    /// <summary>
    /// This is called by Unity every frame. It handles the ships movement and checks if it should fire
    /// </summary>
    void Update() {

		if(isDead) return;

        // move the ship left and right, depending on the horizontal input
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
        if (Input.GetKey(KeyCode.Space) && (lastTimeFired + 1 / rateOfFire) < Time.time) {
            lastTimeFired = Time.time;
            FireTheLasers();
        }
    }

    /// <summary>
    /// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D Collider attached, none them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached. If one of the two 2D Colliders is a trigger, OnTriggerEnter2D(Collider other) is called instead.
    /// </summary>
    void OnCollisionEnter2D(Collision2D col){

        // if the other object has the asteroid tag, the destroy the ship and restard the game
        if(col.gameObject.CompareTag("Asteroid")){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			// load the active scene again, to restard the game. The GameManager will handle this for us. We use a slight delay to see the explosion.
			StartCoroutine(RestartTheGameAfterSeconds(1));
			// we can not destroy the spaceship since it needs to run the coroutine to restart the game.
			// instead, disable update (isDead = true) and remove the renderer to "hide" the object while we reload.
			isDead = true;
			Destroy(GetComponent<SpriteRenderer>());
        }
    }

	/// <summary>
	/// Helper function to include the shooting behavior.
	/// </summary>
    void FireTheLasers(){
        AudioSource.PlayClipAtPoint(laserSound, transform.position);

		// Shooting up
		Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);

		// Shooting left

		// Shooting right
    }

	/// <summary>
	/// Kill all asteroids in the scene
	/// </summary>
	void Bomb(){

	}

	/// <summary>
	/// Wait seconds and reload current scene.
	/// </summary>
	IEnumerator RestartTheGameAfterSeconds(float seconds){
		yield return new WaitForSeconds (seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
