// <copyright file="Powerup.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>02/14/2018</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RapidFirePowerup : Powerup
{
	public float modifier = 2;

	void Start(){
		effect = new RapidFireEffect (modifier);
	}
}

