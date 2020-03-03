using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthPanel : MonoBehaviour
{
	[SerializeField] Image[] hearts;
	public int currentLifes = 8;

	[SerializeField] float removeLifeAnimTimer = 0.5f;

	public void AddLife ()
	{
		currentLifes++;
		hearts [currentLifes - 1].color = new Color (1, 1, 1, 1);
	}

	public void RemoveLife ()
	{
		currentLifes--;
		if (JuiceControl.AnimateHearts) {
			StartCoroutine(RemoveLifeAnimationRoutine (hearts [currentLifes]));
		} else {
			hearts [currentLifes].color = new Color (1, 1, 1, 0.2f);
		}
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
