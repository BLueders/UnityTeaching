// <copyright file="SpaceShip.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the spaceships functionality, including movement and shooting. The spaceship needs any Collider2D attached that has the trigger flag checked, so it can be hit
/// by the asteroids and die.
/// </summary>
public class SpaceShip : MonoBehaviour {

	[Tooltip("How fast is the spaceship moving left to right")]
    public float speed;
	[Tooltip("How fast is the spaceship shooting")]
    public float rateOfFire;
    [Tooltip("Prefab to be instantiated when shooting (Projectile)")]
    public GameObject projectilePrefab;
    [Tooltip("Prefab to be instantiated when dying (explosioin)")]
    public GameObject explosionPrefab;
    [Tooltip("An Audioclip that is played when the ship shoots")]
    public AudioClip laserSound;

    private float lastTimeFired = 0;

    /// <summary>
    /// This is called by Unity every frame. It handles the ships movement and checks if it should fire
    /// </summary>
    void Update() {

        // move the ship left and right, depending on the horizontal input
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // if the fire button is pressed and we waited long enough since the last shot was fired, FIRE!
        if (Input.GetButton("Fire") && (lastTimeFired + 1 / rateOfFire) < Time.time) {
            lastTimeFired = Time.time;
            FireTheLasers();
        } 
    }

    /// <summary>
    /// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D collider attached, at least one of them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other){

        // if the other object has the asteroid tag, the destroy the ship and restard the game
        if(other.CompareTag("Asteroid")){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			// kill the spaceship (not instantly, Destroy() will remove the Gameobject from the scene after this Updatecycle)
			Destroy (gameObject);
			// load the active scene again, to restard the game. The GameManager will handle this for us. We use a slight delay to see the explosion.
			GameManager.instance.RestartTheGameAfterSeconds(1);
        }
    }

	/// <summary>
	/// Helper function to include the shooting behavior.
	/// </summary>
    void FireTheLasers(){
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
		// Create the new projectile a bit in front of the spaceship and store the reference to the new gameobject. 
		// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
		GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity) as GameObject;
		// Get access to the script on the new projectile using GetComponent to modify it.
		Projectile projectile = projectileObject.GetComponent<Projectile> ();
		// Modify the projectile direction?
    }
}
