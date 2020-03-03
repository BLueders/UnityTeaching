using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Transform target = null;
	[SerializeField] float xMin = -10;
	[SerializeField] float xMax = 10;
	[SerializeField] float yMin = -10;
	[SerializeField] float yMax = 10;
	[SerializeField] float lerp = 1;
	[SerializeField] float minSpeed = 1;

	[Header("ScrenShake")]
	[SerializeField] float shakeTimeStandardStrong = 0.5f;
	[SerializeField] float strengthStandardStrong = 0.5f;
	[SerializeField] float shakeTimeStandardLight = 0.2f;
	[SerializeField] float strengthStandardLight = 0.2f;

	float screenShakeStrength = 0;
	float screenShakeTimer = 0;

	static CameraController instance;
	Vector3 offset;

	void Awake ()
	{
		offset = new Vector3 (0, 0, transform.position.z);
		instance = this;
	}

	void Update ()
	{
		Vector3 newPos = transform.position;
		Vector3 targetPosition = target.position + offset;
		Vector3 targetLerp = Vector3.Lerp (newPos, targetPosition, Time.deltaTime * lerp);

		if ((newPos - targetLerp).magnitude > minSpeed * Time.deltaTime) {
			newPos = targetLerp;
		} else if ((newPos - targetPosition).magnitude > minSpeed * Time.deltaTime) {
			Vector3 targetDir = targetPosition - newPos;
			targetDir.Normalize ();
			newPos += targetDir * (Time.deltaTime * minSpeed);	
		} 
		newPos.x = Mathf.Clamp (newPos.x, xMin, xMax);
		newPos.y = Mathf.Clamp (newPos.y, yMin, yMax);

		if (screenShakeTimer > 0) {
			newPos += Random.onUnitSphere * screenShakeStrength;
			screenShakeTimer -= Time.deltaTime;
		}

		transform.position = newPos;
	}

	// Draw outline of movement area when selected in the editor
	// red = max visibility
	// green = max movement
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (new Vector3 (xMin, yMin, 0), new Vector3 (xMin, yMax, 0));
		Gizmos.DrawLine (new Vector3 (xMin, yMax, 0), new Vector3 (xMax, yMax, 0));
		Gizmos.DrawLine (new Vector3 (xMax, yMax, 0), new Vector3 (xMax, yMin, 0));
		Gizmos.DrawLine (new Vector3 (xMax, yMin, 0), new Vector3 (xMin, yMin, 0));
		Gizmos.color = Color.red;
		float xOuterMin = xMin - ((Camera.main.orthographicSize) * Camera.main.aspect);
		float xOuterMax = xMax + ((Camera.main.orthographicSize) * Camera.main.aspect);
		float yOuterMin = yMin - (Camera.main.orthographicSize);
		float yOuterMax = yMax + (Camera.main.orthographicSize);
		Gizmos.DrawLine (new Vector3 (xOuterMin, yOuterMin, 0), new Vector3 (xOuterMin, yOuterMax, 0));
		Gizmos.DrawLine (new Vector3 (xOuterMin, yOuterMax, 0), new Vector3 (xOuterMax, yOuterMax, 0));
		Gizmos.DrawLine (new Vector3 (xOuterMax, yOuterMax, 0), new Vector3 (xOuterMax, yOuterMin, 0));
		Gizmos.DrawLine (new Vector3 (xOuterMax, yOuterMin, 0), new Vector3 (xOuterMin, yOuterMin, 0));
	}

	public static void ScreenShakeLight(){
		instance.screenShakeStrength = instance.strengthStandardLight;
		instance.screenShakeTimer = instance.shakeTimeStandardLight;
	}

	public static void ScreenShakeStrong(){
		instance.screenShakeStrength = instance.strengthStandardStrong;
		instance.screenShakeTimer = instance.shakeTimeStandardStrong;
	}

	public static void ScreenShake(float strength, float shakeTime){
		instance.screenShakeStrength = strength;
		instance.screenShakeTimer = shakeTime;
	}
}
