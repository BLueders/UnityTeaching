// <copyright file="Player.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// Player script. Manages the health and interaction with enemies of the player.
/// </summary>
[RequireComponent (typeof(PlatformerController2D))]
public class Player : MonoBehaviour
{
	public enum PlayerStatus
	{
		Hurt,
		Active,
		InActive,
		Dead
	}

	[Tooltip ("Number of lifes of the player.")]
	[SerializeField] int hitPoints = 8;
	[Tooltip ("Duration of the blinking and stunning when hurt by an enemy.")]
	[SerializeField] float hurtTimer = 0.1f;
	[Tooltip ("Object to be spawned on death.")]
	[SerializeField] GameObject deadPrefab = null;

	PlatformerController2D controller;
	SpriteRenderer[] sr;
	PlayerStatus status;
	Coroutine hurtRoutine;

	void Awake ()
	{
		controller = GetComponent<PlatformerController2D> ();
		sr = GetComponentsInChildren<SpriteRenderer> ();
		status = PlayerStatus.Active;
	}

	/// <summary>
	/// Makes the player jump upwards by force.
	/// </summary>
	/// <param name="force">Strength of upwards push.</param>
	public void ForceJump (float force)
	{
		controller.ForceJump (force);
	}

	/// <summary>
	/// Hurt the Player. The player will lose one hitpoint and is invulnerable for hurtTimer time.
	/// </summary>
	public void Hurt ()
	{
		if (status != PlayerStatus.Active) {
			return;
		}

		hitPoints--;
		UIManager.SetLifes (hitPoints);
		if (hitPoints <= 0) {
			Die ();
			return;
		}
		if (hurtRoutine != null) {
			StopCoroutine (hurtRoutine);
		}
		hurtRoutine = StartCoroutine (HurtRoutine ());
	}

	IEnumerator HurtRoutine ()
	{
		status = PlayerStatus.Hurt;
		float timer = 0;
		bool blink = false;
		while (timer < hurtTimer) {
			blink = !blink;
			timer += Time.deltaTime;
			if (blink) {
				foreach (SpriteRenderer rend in sr) {
					rend.color = Color.white;
				}
			} else {
				foreach (SpriteRenderer rend in sr) {
					rend.color = Color.red;
				}
			}
			yield return new WaitForSeconds (0.05f);
		}
		foreach (SpriteRenderer rend in sr) {
			rend.color = Color.white;
		}
		status = PlayerStatus.Active;
	}

	/// <summary>
	/// Destroy the player and spawn the death animation.
	/// </summary>
	public void Die ()
	{
		Instantiate<GameObject> (deadPrefab, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}

