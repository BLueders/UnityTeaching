using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayMachine : MonoBehaviour {

	public static DelayMachine instance;

	void Awake(){
		instance = this;
	}

	// this would allow delayed execution of a method with no return type and no parameters.
	// You will need to run the coroutine on the instance.
	public static void DelayExecution(float time, Action delayedMethod){
		throw new NotImplementedException ();
	}

	// this would allow delayed execution of a method with no return type and one generic parameter.
	// You will need to run the coroutine on the instance.
	public static void DelayExecution<T>(float time, Action<T> delayedMethod, T param){
		throw new NotImplementedException ();
	}
}
