using UnityEngine;
using System.Collections;

public class ThreeShotEffect : PowerupEffect
{
	public ThreeShotEffect(){
		type = Type.ThreeShot;
	}

	public override void ApplyEffect (SpaceShip target)
	{
		target.fireMode = SpaceShip.FireMode.ThreeShot;
	}

	public override void ResetEffect (SpaceShip target)
	{
		target.fireMode = SpaceShip.FireMode.Normal;
	}
}

