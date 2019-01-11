using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	[SerializeField] UIPlayerHealthPanel healthPanel = null;

	static UIManager instance;

	void Awake(){
		instance = this;
	}

	public static void SetLifes(int lifes){
		while (instance.healthPanel.currentLifes < lifes) {
			instance.healthPanel.AddLife ();
		}
		while (instance.healthPanel.currentLifes > lifes) {
			instance.healthPanel.RemoveLife ();
		}
	}
}
