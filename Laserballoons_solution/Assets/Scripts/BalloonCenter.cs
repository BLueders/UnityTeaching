// <copyright file="BalloonCenter.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this script to rotate the balloons around the center
// Use a Quaternion to rotate the transform in the Update() function
// Adjust the rotation speed using rotationSpeed
public class BalloonCenter : MonoBehaviour {

	public float rotationSpeed = 0; // how fast the balloons are rotating

	void Update(){
		transform.rotation = transform.rotation * Quaternion.AngleAxis (rotationSpeed * Time.deltaTime, Vector3.forward);
	}

}
