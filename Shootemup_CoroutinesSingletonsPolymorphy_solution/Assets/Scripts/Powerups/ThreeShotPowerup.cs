// <copyright file="Powerup.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>02/14/2018</date>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ThreeShotPowerup : Powerup
{
	void Start(){
		effect = new ThreeShotEffect ();
	}
}

