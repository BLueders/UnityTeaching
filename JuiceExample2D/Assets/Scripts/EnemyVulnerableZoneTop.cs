// <copyright file="EnemyVulnerableZoneTop.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// Script to place on a small collider on the top zone of an enemy. When the Player touches that zone, the enemy dies.
/// </summary>
public class EnemyVulnerableZoneTop : MonoBehaviour
{
	[Tooltip ("Enemy this Vulnerable Zone is attached to.")]
	[SerializeField] BaseEnemy enemyObject = null;
	[Tooltip ("How strong is the player bounced upwards when jumping on this.")]
	[SerializeField] float forceJumpStrength = 0;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			enemyObject.Hurt ();
			other.transform.root.GetComponentInChildren<Player> ().ForceJump (forceJumpStrength);
		}
	}
}

