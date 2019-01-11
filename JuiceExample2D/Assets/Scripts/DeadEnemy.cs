using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DeadEnemy : MonoBehaviour {

	public float destroyTimer = 10;
	public float upBounceForce = 5;
	public float gravity = 40;

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
