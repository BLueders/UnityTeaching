// <copyright file="Powerup.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>02/14/2018</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Baser class for all power ups
/// </summary>
public class Powerup : MonoBehaviour
{


	[Tooltip("How long does the powerup last when collected")]
	public float effectDuration = 3;
	[Tooltip("How fast does the powerup move downwards")]
	public float movementSpeed = 1;

	void Update(){
		transform.position = transform.position + new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
	}
}

