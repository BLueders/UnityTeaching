// <copyright file="SimpleNPCInputModule2D.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General controller class for NPCs using the PlatformerController2D.
/// </summary>
[RequireComponent (typeof(PlatformerController2D))]
public class SimpleNPCInputModule2D : MonoBehaviour
{
	[Tooltip ("Is this NPC able to move.")]
	public bool canMove = true;

	PlatformerController2D controller;
	int dir = 1;

	[Tooltip ("Location of the downwards raycast to check if there is ground on the left. If not, the NPC will turn around. Position at lower left corner of NPC collider.")]
	[SerializeField] Transform leftGroundCheck = null;
	[Tooltip ("Location of the downwards raycast to check if there is ground on the right. If not, the NPC will turn around. Position at lower right corner of NPC collider.")]
	[SerializeField] Transform rightGroundCheck = null;
	[Tooltip ("Trigger to be placed slightly to the left of the NPC collider. A collision will cause the NPC to turn around.")]
	[SerializeField] Collider2D leftWallCheck = null;
	[Tooltip ("Trigger to be placed slightly to the right of the NPC collider. A collision will cause the NPC to turn around.")]
	[SerializeField] Collider2D rightWallCheck = null;
	[Tooltip ("Layers to be considered ground for groundchecks and collision checks when checking for change of direction.")]
	[SerializeField] LayerMask groundLayers = 0;

	void Start ()
	{
		controller = GetComponent<PlatformerController2D> ();
	}

	void Update ()
	{
		if (!canMove) {
			controller.input = new Vector2 (0, 0);
			return;
		}

		if (controller.IsGrounded) {
			RaycastHit2D hitLeft = Physics2D.Linecast (new Vector2 (leftGroundCheck.position.x, leftGroundCheck.position.y), new Vector2 (leftGroundCheck.position.x, leftGroundCheck.position.y - 0.1f), groundLayers);
			RaycastHit2D hitRight = Physics2D.Linecast (new Vector2 (rightGroundCheck.position.x, rightGroundCheck.position.y), new Vector2 (rightGroundCheck.position.x, rightGroundCheck.position.y - 0.1f), groundLayers);
			if (hitLeft.collider == null) {
				dir = 1;
			}
			if (hitRight.collider == null) {
				dir = -1;
			}
		}

		if (leftWallCheck.IsTouchingLayers (groundLayers)) {
			dir = 1;
		}

		if (rightWallCheck.IsTouchingLayers (groundLayers)) {
			dir = -1;
		}

		if (controller.IsGrounded) {
			controller.input = new Vector2 (dir, 0);
		} else {
			controller.input = new Vector2 (0, 0);
		}
	}
}

