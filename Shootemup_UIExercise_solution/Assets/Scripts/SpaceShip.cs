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
	[Tooltip("Maximum HP of the spaceship. Each Asteroid deals 1 damage.")]
	public int maxHP = 4;
	[Tooltip("Maximum Bombs of the spaceship.")]
	public int maxBombs = 4;

    private float lastTimeFired = 0;
	private UIHealthPanel hpanel;
	private int hp;
	private int bombs;

	void Start(){
		hpanel = GameObject.FindObjectOfType<UIHealthPanel> ();
		if (hpanel == null) {
			Debug.LogError ("UIHealthPanel component could not be found, add it to the UI.");
		}
		bombs = maxBombs;
		hp = maxHP;
		hpanel.SetLives (maxHP, hp);
	}

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

		if (Input.GetKeyDown (KeyCode.LeftControl) && bombs > 0) {
			bombs--;
			Bomb ();
		}
    }

    /// <summary>
    /// This is called by Unity when the object collides with another object. It is only called, when both objects have any 2D collider attached, at least one of them is a trigger and at least of of
    /// the two colliding GameObjects has a Rigidbody2D attached.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other){

        // if the other object has the asteroid tag, the destroy the ship and restard the game
        if(other.CompareTag("Asteroid")){
			other.GetComponent<Asteroid> ().Die ();
			Hurt ();
        }
    }

	void Hurt(){
		hp--;
		hpanel.SetLives (maxHP, hp);
		if (hp <= 0) {
			Die ();
		}
	}

	void Die(){
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		// kill the spaceship (not instantly, Destroy() will remove the Gameobject from the scene after this Updatecycle)
		Destroy (gameObject);
		// load the active scene again, to restard the game. The GameManager will handle this for us. We use a slight delay to see the explosion.
		GameManager.instance.RestartTheGameAfterSeconds(1);
	}

	/// <summary>
	/// Helper function to include the shooting behavior.
	/// </summary>
    void FireTheLasers(){
        AudioSource.PlayClipAtPoint(laserSound, transform.position);

		// Shooting up
		Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);

		// Shooting left
		GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up + Vector3.left, Quaternion.identity) as GameObject;
		Projectile projectile = projectileObject.GetComponent<Projectile> ();
		projectile.direction.x = -0.5f;

		// Shooting right
		projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up + Vector3.right, Quaternion.identity) as GameObject;
		projectile = projectileObject.GetComponent<Projectile> ();
		projectile.direction.x = 0.5f;
    }

	/// <summary>
	/// Kill all asteroids in the scene
	/// </summary>
	void Bomb(){
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		foreach (GameObject astr in asteroids) {
			astr.GetComponent<Asteroid> ().Die ();
		}
	}
}
