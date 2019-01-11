using UnityEngine;
using System.Collections;

public class RapidFireEffect : PowerupEffect
{
	public float modifier = 2;

	public RapidFireEffect(float modifier){
		type = Type.RapidFire;
		this.modifier = modifier;
	}

	public override void ApplyEffect (SpaceShip target)
	{
		target.rateOfFire = target.rateOfFire * modifier;
	}

	public override void ResetEffect (SpaceShip target)
	{
		target.rateOfFire = target.rateOfFire / modifier;
	}
}

