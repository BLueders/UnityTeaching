using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlatformerController))]
public class Player : MonoBehaviour
{
	public enum PlayerStatus
	{
		Hurt,
		Active,
		InActive,
		Dead
	}

	[SerializeField] int hitPoints = 8;
	[SerializeField] float hurtTimer = 0.1f;
	[SerializeField] float pushBackStun = 0.5f;

	PlatformerController controller;
	SpriteRenderer[] sr;
	PlayerStatus status;
	Coroutine hurtRoutine;

	void Awake ()
	{
		controller = GetComponent<PlatformerController> ();
		sr = GetComponentsInChildren<SpriteRenderer> ();
		status = PlayerStatus.Active;
	}

	public void ForceJump (float force)
	{
		controller.ForceJump (force);
	}

	public void Pushback (Vector2 force)
	{
		controller.Pushback (force);
		StartCoroutine (PushBackRoutine ());
	}

	public void Hurt ()
	{
		if (status == PlayerStatus.Hurt) {
			return;
		}

		hitPoints--;
		UIManager.SetLifes (hitPoints);
		if (hitPoints <= 0) {
			Die ();
			return;
		}

		if (JuiceControl.ScreenShake) {
			CameraController.ScreenShakeStrong ();
		}

		if (hurtRoutine != null) {
			StopCoroutine (hurtRoutine);
		}
		hurtRoutine = StartCoroutine (HurtRoutine ());
	}

	IEnumerator PushBackRoutine(){
		controller.canMove = false;
		yield return new WaitForSeconds(pushBackStun);
		controller.canMove = true;
	}

	IEnumerator HurtRoutine ()
	{
		status = PlayerStatus.Hurt;
		float timer = 0;
		bool blink = false;
		if (JuiceControl.PlayerHitFeedback) {
			while (timer < hurtTimer) {
				blink = !blink;
				timer += Time.deltaTime;
				if (blink) {
					foreach (SpriteRenderer rend in sr) {
						rend.color = Color.white;
					}
				} else {
					foreach (SpriteRenderer rend in sr) {
						rend.color = Color.red;
					}
				}
				yield return new WaitForSeconds (0.05f);
			}
			foreach (SpriteRenderer rend in sr) {
				rend.color = Color.white;
			}
		}
		status = PlayerStatus.Active;
	}

	public void Die ()
	{
		Destroy (gameObject);
	}
}

