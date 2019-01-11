using UnityEngine;
using System.Collections;

public abstract class PowerupEffect
{
	public enum Type
	{
		RapidFire, ThreeShot
	}

	public Type type;
	public abstract void ApplyEffect (SpaceShip target);
	public abstract void ResetEffect (SpaceShip target);
}

