using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SimpleNPCInputModule))]
public class SnakeEnemy : BaseEnemy
{
	[SerializeField] int hitPoints = 3;
	[SerializeField] LayerMask playerLayer = 0;
	[SerializeField] float hurtTimer = 0.1f;
	[SerializeField] float pushBackForce = 20;
	[SerializeField] GameObject deadPrefab = null;
	[SerializeField] GameObject bloodPrefab = null;

	Coroutine hurtRoutine;
	EnemyStatus status;
	SpriteRenderer[] sr;
	SimpleNPCInputModule inputModule = null;
	int maxHitPoints;

	void Awake ()
	{
		maxHitPoints = hitPoints;
		sr = GetComponentsInChildren<SpriteRenderer> ();
		inputModule = GetComponent<SimpleNPCInputModule> ();
		status = EnemyStatus.Active;
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (status == EnemyStatus.Active && col.collider.CompareTag ("Player")) {
			Player player = col.transform.root.GetComponentInChildren<Player> ();
			player.Hurt ();

			Vector2 colNormal = col.transform.position - transform.position;
			colNormal.Normalize ();
			if (JuiceControl.PlayerHitFeedback) {
				player.Pushback (colNormal * pushBackForce);
			} else {
				player.Pushback (colNormal * pushBackForce/10.0f);
			}
		}
	}

	public override void Hurt ()
	{
		hitPoints--;
		if (hitPoints <= 0) {
			Die ();
			return;
		}

		if (hurtRoutine != null) {
			StopCoroutine (hurtRoutine);
		}
		hurtRoutine = StartCoroutine (HurtRoutine ());
	}

	IEnumerator HurtRoutine ()
	{
		status = EnemyStatus.Hurt;
		inputModule.canMove = false;
		float timer = 0;
		bool blink = false;

		if (JuiceControl.EnemyHitFeedback) {
			
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
		}
		foreach (SpriteRenderer rend in sr) {
			rend.color = Color.white;
		}
		status = EnemyStatus.Active;
		inputModule.canMove = true;
	}

	public void Die ()
	{
		if (JuiceControl.EnemyDeathAnim) {
			GameObject deadSnake = Instantiate<GameObject> (deadPrefab, transform.position, transform.rotation);
		}
		if (JuiceControl.EnemyDeathPermanence) {
			GameObject deadSnake = Instantiate<GameObject> (bloodPrefab, transform.position, Quaternion.AngleAxis (Random.Range (0, 360), Vector3.forward));
		}
		if (JuiceControl.ScreenShake) {
			CameraController.ScreenShakeLight ();
		}
		Destroy (gameObject);
	}
}
