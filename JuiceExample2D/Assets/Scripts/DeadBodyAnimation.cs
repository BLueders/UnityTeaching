// <copyright file="DeadBodyAnimation.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation script to put on objects that pop up and fall out of the screen Super Mario style. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class DeadBodyAnimation : MonoBehaviour {

	[Tooltip ("Timer to remove this from the scene after beeing spawned.")]
	[SerializeField] float destroyTimer = 10;
	[Tooltip ("When spawned the item gets pushed up and then falls down. Determines the push up force.")] 
	[SerializeField] float upBounceForce = 5;
	[Tooltip ("Acceleration downwards out of the sceen.")]
	[SerializeField] float gravity = 40;

	Rigidbody2D rb2d;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (DestroyRoutine ());
		rb2d.velocity = new Vector2 (0, upBounceForce);
	}

	void Update(){
		Vector2 vel = rb2d.velocity;
		vel.y -= gravity * Time.deltaTime;
		rb2d.velocity = vel;
	}

	IEnumerator DestroyRoutine(){
		yield return new WaitForSeconds (destroyTimer);
		Destroy (gameObject);
	}

}
