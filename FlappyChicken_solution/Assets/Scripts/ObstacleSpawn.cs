/*
 * Created on Mon Jun 18 2018
 *
 * Copyright (c) 2018 DIS
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Periodically spawn obstacles at its own location, plus a set offset.
/// </summary>
public class ObstacleSpawn : MonoBehaviour
{
    public GameObject obstaclePrefabUp;
    public GameObject obstaclePrefabDown;

    // minimum and maximum offset up and down for our obstacles
    public float distanceMin;
    public float distanceMax;

    public float spawnTimer;
    public float startDelay;

    void Start()
    {
        timer = startDelay;
    }

    private float timer;

    // Update is called once per frame
    void Update()
    {
        // The timer will count down until it is lower that 0, then spawn an obstacle and reset
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnNewObstacle();
            timer = spawnTimer;
        }
    }

    /// <summary>
    /// Spawns two obstacles (up and down)
    /// </summary>
    private void SpawnNewObstacle()
    {
        Vector3 spawnPositionDown = transform.position + Vector3.down * Random.Range(distanceMin, distanceMax);
        Vector3 spawnPositionUp = transform.position + Vector3.up * Random.Range(distanceMin, distanceMax);
        GameObject bottomObstacle = Instantiate<GameObject>(obstaclePrefabDown, spawnPositionDown, Quaternion.identity);
        GameObject topObstacle = Instantiate<GameObject>(obstaclePrefabUp, spawnPositionUp, Quaternion.identity);
    }
}
