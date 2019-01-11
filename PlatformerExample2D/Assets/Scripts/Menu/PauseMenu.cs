// <copyright file="PauseMenu.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pause menu scipt. Handles the setting of the time scale when paused and button presses for loading scenes and quiting the game.
/// </summary>
public class PauseMenu : MonoBehaviour
{
	public enum Status
	{
		Active,
		Inactive
	}

	[Tooltip ("Panel with the menu items on them. Gets enabled and disabled.")]
	[SerializeField] GameObject UIPanel = null;

	Status status;

	void Start ()
	{
		status = Status.Inactive;
		Time.timeScale = 1;
		UIPanel.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (status == Status.Active) {
				Time.timeScale = 1;
				Close ();
			} else if (status == Status.Inactive) {
				Time.timeScale = 0;
				Open ();
			}
		}
	}

	/// <summary>
	/// Open the Pause Menu and pause the game.
	/// </summary>
	public void Open ()
	{
		status = Status.Active;
		UIPanel.SetActive (true);
	}

	/// <summary>
	/// Close the Pause Menu and unpause the game.
	/// </summary>
	public void Close ()
	{
		status = Status.Inactive;
		UIPanel.SetActive (false);
	}

	/// <summary>
	/// Loads the scene.
	/// </summary>
	/// <param name="scene">scene to load</param>
	public void LoadScene(string scene){
		SceneManager.LoadScene (scene);
	}
}
