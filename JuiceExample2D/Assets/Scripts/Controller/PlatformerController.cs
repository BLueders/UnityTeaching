using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
	public Vector2 input;
	public bool inputJump;
	public bool canMove = true;

	public bool IsGrounded { get { return grounded; } }

	[SerializeField] float speed = 5;
	[SerializeField] float jumpVelocity = 15;
	[SerializeField] float gravity = 40;
	[SerializeField] float groundingTolerance = .1f;
	[SerializeField] float jumpingTolerance = .1f;

	[SerializeField] CircleCollider2D groundCollider = null;
	[SerializeField] LayerMask groundLayers = 0;

	bool grounded;
	Rigidbody2D rb2d;
	SpriteRenderer sr;
	Animator anim;

	float lostGroundingTime;
	float lastJumpTime;
	float lastInputJump;
	int facing = 1;

	void Start ()
	{
		canMove = true;
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate ()
	{
		grounded = CheckGrounded ();

		Vector2 vel = rb2d.velocity;

		if (canMove) {
			vel.x = input.x * speed;

			if (CheckJumpInput () && PermissionToJump ()) {
				vel = ApplyJump (vel);
			}
		}

		vel.y += -gravity * Time.deltaTime;
		rb2d.velocity = vel;

		UpdateAnimations ();
	}

	Vector2 ApplyJump (Vector2 vel)
	{
		vel.y = jumpVelocity;
		lastJumpTime = Time.time;
		grounded = false;
		return vel;
	}

	bool CheckGrounded ()
	{
		if (groundCollider.IsTouchingLayers (groundLayers)) {
			lostGroundingTime = Time.time;
			return true;
		}
		return false;
	}

	void UpdateAnimations ()
	{
		if (!canMove) {
			anim.SetBool ("grounded", false);
			return;
		}
		if (rb2d.velocity.x > 0 && facing == -1) {
			facing = 1;
		} else if (rb2d.velocity.x < 0 && facing == 1) {
			facing = -1;
		}
		sr.flipX = facing == -1;
		anim.SetBool ("grounded", grounded);
		anim.SetFloat ("speed", Mathf.Abs (rb2d.velocity.x));
		if (lastJumpTime == Time.time) {
			anim.SetTrigger ("jump");
		}
	}

	bool PermissionToJump ()
	{
		bool wasJustgrounded = Time.time < lostGroundingTime + groundingTolerance;
		bool hasJustJumped = Time.time < lastJumpTime + groundingTolerance + Time.deltaTime;
		return (grounded || wasJustgrounded) && !hasJustJumped;
	}

	bool CheckJumpInput ()
	{
		if (inputJump) {
			lastInputJump = Time.time;
			return true;
		}
		if (Time.time < lastInputJump + jumpingTolerance) {
			return true;
		}
		return false;
	}

	public void Pushback (Vector2 force)
	{
		rb2d.velocity = force;
		lastJumpTime = Time.time;
		grounded = false;
	}

	public void ForceJump (float strength)
	{
		rb2d.velocity = new Vector2 (rb2d.velocity.x, strength);
		lastJumpTime = Time.time;
		grounded = false;
	}
}
