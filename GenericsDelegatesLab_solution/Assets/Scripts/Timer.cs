using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text timerText;	

	void Update () {
		int milliseconds = 0;
		int seconds = 0;
		int minutes = 0;
		int hours = 0;

		seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad);
		milliseconds = Mathf.FloorToInt ((Time.timeSinceLevelLoad - seconds) * 1000);
		minutes = seconds / 60;
		hours = minutes / 60;

		string timeString = hours.ToString ("D2") + ":" + minutes.ToString ("D2") + ":" + seconds.ToString ("D2") + "." + milliseconds.ToString("D3");
		timerText.text = timeString;
	}
}
