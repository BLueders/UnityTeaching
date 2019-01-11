// <copyright file="SnakeEnemy.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the snake type enemy, that can be killed when jumping on its head.
/// </summary>
[RequireComponent (typeof(SimpleNPCInputModule2D))]
public class SnakeEnemy : BaseEnemy
{
	[Tooltip ("GameObject to be spawned when this instance dies.")]
	[SerializeField] GameObject deadPrefab = null;

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.collider.CompareTag ("Player")) {
			Player player = col.transform.root.GetComponentInChildren<Player> ();
			player.Hurt ();
		}
	}
		
	public override void Die ()
	{
		Instantiate<GameObject> (deadPrefab, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
