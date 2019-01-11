using UnityEngine;
using System.Collections;

public enum EnemyStatus
{
	Hurt,
	Active,
	InActive,
	Dead
}

public abstract class BaseEnemy : MonoBehaviour
{
	public abstract void Hurt ();
}

