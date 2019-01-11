using UnityEngine;
using System.Collections;

public class EnemyVulnerableZoneTop : MonoBehaviour
{
	[SerializeField] BaseEnemy enemyObject = null;
	[SerializeField] float forceJumpStrength = 0;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			enemyObject.Hurt ();
			other.transform.root.GetComponentInChildren<Player> ().ForceJump (forceJumpStrength);
		}
	}
}

