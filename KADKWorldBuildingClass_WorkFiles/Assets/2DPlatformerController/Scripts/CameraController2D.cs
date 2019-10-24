// <copyright file="CameraController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for a playerfocused 2D camera.
/// </summary>
public class CameraController2D : MonoBehaviour
{
	[Tooltip ("The target transform to follow.")]
	[SerializeField] Transform target = null;
	[Tooltip ("Global minimum x value of the cameras position.")]
	[SerializeField] float xMin = -10;
	[Tooltip ("Global maximum x value of the cameras position.")]
	[SerializeField] float xMax = 10;
	[Tooltip ("Global minimum y value of the cameras position.")]
	[SerializeField] float yMin = -10;
	[Tooltip ("Global maximum y value of the cameras postion.")]
	[SerializeField] float yMax = 10;
	[Tooltip ("duration of the camera to line up with the target.")]
	[SerializeField] float lerp = 1;
	[Tooltip ("minimum speed of the camera to follow the target.")]
	[SerializeField] float minSpeed = 1;

	Vector3 offset;

	void Awake ()
	{
		offset = new Vector3 (0, 0, transform.position.z);
	}

	void Update ()
	{
		if (target == null) {
			return;
		}

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
}
