using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlatformerController))]
public class PlayerInputModule : MonoBehaviour
{
	PlatformerController controller;

	void Start ()
	{
		controller = GetComponent<PlatformerController> ();
	}

	void FixedUpdate ()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if (input.magnitude > 1) {
			input.Normalize ();
		}
		controller.input = input;
		controller.inputJump = Input.GetButtonDown ("Jump");
	}
}
