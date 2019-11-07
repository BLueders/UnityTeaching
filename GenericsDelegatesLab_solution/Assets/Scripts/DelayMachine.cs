using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayMachine : Singleton<DelayMachine> {

	// this would allow delayed execution of a method with no return type and no parameters.
	public static void DelayExecution(float time, Action delayedMethod){
		Instance.StartCoroutine (Instance.DelayedExecutionRoutine (time, delayedMethod));
	}

	// this would allow delayed execution of a method with no return type and one generic parameter.
	public static void DelayExecution<T>(float time, Action<T> delayedMethod, T param){
		Instance.StartCoroutine (Instance.DelayedExecutionRoutine (time, delayedMethod, param));
	}

	IEnumerator DelayedExecutionRoutine(float time, Action delayedMethod){
		yield return new WaitForSeconds (time);
		delayedMethod ();
	}

	IEnumerator DelayedExecutionRoutine<T>(float time, Action<T> delayedMethod, T param){
		yield return new WaitForSeconds (time);
		delayedMethod (param);
	}
}
