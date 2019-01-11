// <copyright file="UIPlayerHealthPanel.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User interface manager. To set and manage the heart display of the player lifes. Is a singleton for easy access.
/// </summary>
public class UIManager : MonoBehaviour {

	[Tooltip ("Instance of the health panel script to manage.")]
	[SerializeField] UIPlayerHealthPanel healthPanel = null;

	static UIManager instance;

	void Awake(){
		instance = this;
	}

	/// <summary>
	/// Set the number of lifes to be displayed with hearts on the UI.
	/// </summary>
	/// <param name="lifes">Lifes.</param>
	public static void SetLifes(int lifes){
		while (instance.healthPanel.currentLifes < lifes) {
			instance.healthPanel.AddLife ();
		}
		while (instance.healthPanel.currentLifes > lifes) {
			instance.healthPanel.RemoveLife ();
		}
	}
}
