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
/// This class uses the singleton pattern to provide easy access to the outside through the public static instance variable.
/// </summary>
public class GameManager : MonoBehaviour {

	// Varibale to store our game manager instance. We need this in order to use instance only functionality like StartCoroutine() for example
	public static GameManager instance;

	/// <summary>
	/// Use Awake to initialize the instance with the active script instance from the current scene.
	/// </summary>
	void Awake(){
		instance = this;
	}

	/// <summary>
	/// Reloads the current scene with some delay in seconds.
	/// </summary>
	/// <param name="seconds">Seconds.</param>
	public void RestartTheGameAfterSeconds(float seconds){
		StartCoroutine (LoadSceneAfterSeconds (seconds));
	}

	// Coroutine to start the game again
	IEnumerator LoadSceneAfterSeconds(float seconds){
		yield return new WaitForSeconds (seconds);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}

