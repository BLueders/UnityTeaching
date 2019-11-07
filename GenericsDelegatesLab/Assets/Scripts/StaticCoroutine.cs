using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Exercise 1, write a class that allows you to run coroutines in a static context using delegates as callbacks, when the routine finishes.
// Use a singleton pattern to get access to a GameObject to run the coroutines on.
public class StaticCoroutine : MonoBehaviour {

	public static StaticCoroutine instance;

	void Awake(){
		instance = this;
	}

	// This function is called from the outside to start a coroutine defined anywhere in the callers context.
	// You will need to run the coroutine on the instance.
	public static void RunCoroutine(IEnumerator routine){
		throw new NotImplementedException ();
	}

	// This function is called from the outside to start a coroutine defined anywhere in the callers context. 
	// It should also forward the callback delegate to the coroutine, so it can be executed when finished.
	// You will need to run the coroutine on the instance.
	public static void RunCoroutine(IEnumerator routine, Action onComplete){
		throw new NotImplementedException ();
	}
}
