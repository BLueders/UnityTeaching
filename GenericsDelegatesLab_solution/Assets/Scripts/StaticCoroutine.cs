using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Exercise 1, write a class that allows you to run coroutines in a static context using delegates as callbacks, when the routine finishes.
// Use a singleton pattern to get access to a GameObject to run the coroutines on.
public class StaticCoroutine : Singleton<StaticCoroutine> {

	// This function is called from the outside to start a coroutine defined anywhere in the callers context.
	public static void RunCoroutine(IEnumerator routine){
		Instance.StartCoroutine (routine);		
	}

	// This function is called from the outside to start a coroutine defined anywhere in the callers context. 
	// It should also forward the callback delegate to the coroutine, so it can be executed when finished.
	public static void RunCoroutine(IEnumerator routine, Action onComplete){
		Instance.StartCoroutine (Instance.WaitForCallbackRoutine(routine, onComplete));	
	}

	private IEnumerator WaitForCallbackRoutine(IEnumerator routine, Action onComplete){
		Coroutine r = StartCoroutine (routine);
		yield return r;
		onComplete ();
	}
}
