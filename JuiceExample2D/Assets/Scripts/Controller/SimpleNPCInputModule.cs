using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlatformerController))]
public class SimpleNPCInputModule : MonoBehaviour
{
	public bool canMove = true;

	PlatformerController controller;
	int dir = 1;
	[SerializeField] Transform leftGroundCheck = null;
	[SerializeField] Transform rightGroundCheck = null;
	[SerializeField] Collider2D leftWallCheck = null;
	[SerializeField] Collider2D rightWallCheck = null;
	[SerializeField] LayerMask groundLayers = 0;

	void Start ()
	{
		controller = GetComponent<PlatformerController> ();
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

