// <copyright file="GameManager.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Almost every game project includes some kind of Game Manager class that handles the overall flow of the game, namely loading and transitioning through scenes.
/// </summary>
public class GameManager : MonoBehaviour {

	/// <summary>
	/// This GameManager will check for input to restart the scene
	/// </summary>
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			RestartTheGame ();
		}
	}
		
	/// <summary>
	/// Reloads the current scene.
	/// </summary>
	public void RestartTheGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

}

