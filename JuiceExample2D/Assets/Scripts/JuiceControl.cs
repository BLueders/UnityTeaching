using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceControl : MonoBehaviour
{

	static JuiceControl instance;

	public static bool PauseMenuFadeIn { get { return instance.pauseMenuFadeIn; } }
	public static bool PlayerHitFeedback { get { return instance.playerHitFeedback; } }
	public static bool ScreenShake { get { return instance.screenShake; } }
	public static bool AnimateHearts { get { return instance.animateHearts; } }
	public static bool EnemyDeathAnim { get { return instance.enemyDeathAnim; } }
	public static bool EnemyDeathPermanence { get { return instance.enemyDeathPermanence; } }
	public static bool EnemyHitFeedback { get { return instance.enemyHitFeedback; } }

	[SerializeField] bool pauseMenuFadeIn = false;
	[SerializeField] bool screenShake = false;
	[SerializeField] bool animateHearts = false;
	[SerializeField] bool enemyDeathAnim = false;
	[SerializeField] bool enemyDeathPermanence = false;
	[SerializeField] bool enemyHitFeedback = false;
	[SerializeField] bool playerHitFeedback = false;

	void Start ()
	{
		instance = this;
	}
}
