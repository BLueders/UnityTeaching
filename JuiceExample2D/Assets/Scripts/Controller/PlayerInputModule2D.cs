// <copyright file="PlayerInputModule2D.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Module for player controller input when using a PlatformController2D. Uses standart Horizontal and Vertical input axis as well as Jump button.
/// </summary>
[RequireComponent (typeof(PlatformerController2D))]
public class PlayerInputModule2D : MonoBehaviour
{
	PlatformerController2D controller;

	void Start ()
	{
		controller = GetComponent<PlatformerController2D> ();
	}

	void Update ()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if (input.magnitude > 1) {
			input.Normalize ();
		}
		controller.input = input;
        if (Input.GetButtonDown("Jump"))
        {
            controller.inputJump = true;
        }
	}
}
