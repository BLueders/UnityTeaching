using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPanel : MonoBehaviour
{
	[SerializeField] Text healthText;
	[SerializeField] Image[] hearts;
	[SerializeField] RectTransform healthSlider;

	public void SetLives (int maxLives, int lives)
	{
		UpdateText (lives);
		UpdateHearts (lives);
		UpdateSlider (maxLives, lives);
	}

	/// <summary>
	/// Updates the text simply by setting it to the number of lives.
	/// </summary>
	/// <param name="lives">Lives.</param>
	void UpdateText(int lives){
		if (healthText == null)
			return;
		string format = "D2"; // D2 means decimal format with a minimum of 2 digits, for one digit numbers this will give a preceding 0;
		healthText.text = lives.ToString (format);
	}

	/// <summary>
	/// Updates the hearts by modifying its image component depending of the number of lives.
	/// </summary>
	/// <param name="lives">Lives.</param>
	void UpdateHearts(int lives){
		for (int i = 0; i < hearts.Length; i++) 
		{
			if (i < lives) {
				hearts [i].enabled = true;
			} else {
				hearts [i].enabled = false;
			}
		}
	}

	/// <summary>
	/// Updates the slider using its x-scale.
	/// </summary>
	/// <param name="lives">Lives.</param>
	void UpdateSlider(int maxLives, int lives){
		if (healthSlider == null)
			return;
		float relativeScale = (float)lives / (float)maxLives;
		Vector3 scale = healthSlider.transform.localScale;
		scale.x = relativeScale;
		healthSlider.transform.localScale = scale;
	}
}
