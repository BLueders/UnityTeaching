// <copyright file="Balloon.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is on each balloon and will handle the balloon destruction
// From the soldier script, call Death() on the balloons to kill them, if hit
public class Balloon : MonoBehaviour {

	public GameObject poofPrefab;

	public void Pop(){
		if (poofPrefab != null) {
			Instantiate (poofPrefab, transform.position, Quaternion.identity);
		}
		Destroy (gameObject);
	}
}
