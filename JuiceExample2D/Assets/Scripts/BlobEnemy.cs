using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleNPCInputModule2D))]
[RequireComponent(typeof(PlatformerController2D))]
public class BlobEnemy : BaseEnemy
{
	[SerializeField] LayerMask playerLayer = 0;
	[SerializeField] float pushBackForce = 20;
	[SerializeField] float hurtTimer = 1;
	[SerializeField] AnimationCurve bounceAnim;

	Coroutine hurtRoutine;
	EnemyStatus status;
	SpriteRenderer[] sr;
	SimpleNPCInputModule2D inputModule = null;

	void Awake ()
	{
		sr = GetComponentsInChildren<SpriteRenderer> ();
		inputModule = GetComponent<SimpleNPCInputModule2D> ();
		status = EnemyStatus.Active;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (status == EnemyStatus.Active && col.collider.CompareTag ("Player")) {
			Player player = col.transform.root.GetComponentInChildren<Player> ();
			Vector2 colNormal = col.transform.position - transform.position;
			colNormal.Normalize ();
			player.Pushback (colNormal * pushBackForce);
			Hurt ();
		}
	}

	public override void Hurt ()
	{
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
		if (JuiceControl.EnemyHitFeedback) {
			while (timer < hurtTimer / hurtTimer) {
				float normalizedTime = timer / hurtTimer;
				timer += Time.deltaTime;
				float scale = bounceAnim.Evaluate (normalizedTime);
				transform.localScale = new Vector3 (scale, scale, scale);
				yield return null;
			}
		}
		transform.localScale = new Vector3 (1, 1, 1);
		status = EnemyStatus.Active;
		inputModule.canMove = true;
	}

    public override void Die()
    {
        Destroy(gameObject);
    }
}
