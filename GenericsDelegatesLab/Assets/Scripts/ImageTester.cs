using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class allows you to test the various tools you will make in this lab. 
/// It simply provides a couple of functions to swap the image on the UI with the testSprite image.
/// There is also a default callback function that just prints out
/// </summary>
public class ImageTester : MonoBehaviour {

	public Image uiImage;
	public Sprite testSprite1;
	public Sprite testSprite2;

	void Start(){
		// Pay attention that only one of these lines is active at all times

		//Exercise 1b) StaticCoroutine.RunCoroutine(SwapeImageRoutine(5.0f, testSprite1));
		//Exercise 1c) StaticCoroutine.RunCoroutine(WaitForAnyKeyRoutine(), SwapImage);

		//Exercise 2b) DelayMachine.DelayExecution(5.0f, SwapImage);
		//Exercise 2c) DelayMachine.DelayExecution(5.0f, SwapImageWithTarget, testSprite1);

		//Exercise 4) ResourceManager.LoadSpriteRessource ("http://benno-lueders.de/img/victory_yoda.jpg", SwapImageWithTarget);
	}

	public void SwapImage(){
		uiImage.sprite = testSprite2;
	}

	public void SwapImageWithTarget(Sprite target){
		uiImage.sprite = target;
	}

	public IEnumerator SwapeImageRoutine(float secondsToWait, Sprite target){
		yield return new WaitForSeconds (secondsToWait);
		SwapImageWithTarget (target);
	}

	public IEnumerator WaitForAnyKeyRoutine(){
		while (!Input.anyKey) {
			yield return null;
		}
	}

	// you can use this as a dummy for callback functions
	public void LogCallback(){
		Debug.Log ("Function was executed");
	}

	// you can use this as a dummy for callback functions with a string argument
	public void LogCallback(string message){
		Debug.Log ("Function was executed with message: " + message);
	}
}
