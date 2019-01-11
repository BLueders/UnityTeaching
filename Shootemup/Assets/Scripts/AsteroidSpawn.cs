// <copyright file="AsteroidSpawn.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// Asteroid spawn is used to spawn the asteroids from the top of the screen. Place this script on an empty GameObject on top of the screen.
/// </summary>
public class AsteroidSpawn : MonoBehaviour {

    [Tooltip("A prefab that is instantiated when the asteroid is destroyed")]
    public float spawnWidth;
    [Tooltip("How many asteroids spawn per second?")]
    public float spawnRate;
    [Tooltip("The prefab that is to be instantiated as asteroids")]
    public GameObject AsteroidPrefab;

    private float lastSpawnTime = 0;

    /// <summary>
    /// Update is called by Unity. This will spawn asteroids while the game is in play mode.
    /// </summary>
    void Update() {
        // this is a simple timer structure that executes every 1/spawnRate seconds. This means it spawns spawnRate asteroids every second.
        if (lastSpawnTime + 1 / spawnRate < Time.time) {
            lastSpawnTime = Time.time;
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
			// the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
            Instantiate(AsteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }

	/// <summary>
	/// Helper function called by unity to draw gizmos for debugging and orientation in the scene view. Is not part of any game logic.
	/// </summary>
	void OnDrawGizmos(){
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 0, 0), transform.position + new Vector3 (spawnWidth, 0, 0));
		Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 1, 0), transform.position - new Vector3 (spawnWidth, -1, 0));
		Gizmos.DrawLine (transform.position + new Vector3 (spawnWidth, 1, 0), transform.position + new Vector3 (spawnWidth, -1, 0));
	}
}
