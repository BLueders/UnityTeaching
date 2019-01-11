// <copyright file="UIPlayerHealthPanel.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthPanel : MonoBehaviour
{
	[Tooltip ("UI images of the hearts, number of hearts has to be equal to the number of lifes!")]
	[SerializeField] Image[] hearts = null;

	[Tooltip ("How many hearts are currently shown active")]
	public int currentLifes = 8;

	[Tooltip ("How long is the blinking animation when a heart is lost")]
	[SerializeField] float removeLifeAnimTimer = 0.5f;

	public void AddLife ()
	{
		currentLifes++;
		hearts [currentLifes - 1].color = new Color (1, 1, 1, 1);
	}

	public void RemoveLife ()
	{
		currentLifes--;
		hearts [currentLifes].color = new Color (1, 1, 1, 0.2f);
	}

	IEnumerator RemoveLifeAnimationRoutine (Image sr)
	{
		float timer = 0;
		bool blink = false;
		while (timer < removeLifeAnimTimer) {
			blink = !blink;
			timer += Time.deltaTime;
			if (blink) {
				sr.color = Color.white;
			} else {
				sr.color = Color.black;
			}
			yield return new WaitForSeconds (0.05f);
		}
		sr.color = new Color (1, 1, 1, 0.2f);
	}
}
