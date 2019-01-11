// <copyright file="CameraController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>05/10/2017</date>

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

	public LayerMask obstacleLayerMask;

    public float distance = 10;
    public float minVerticalAngle = -80;
    public float maxVerticalAngle = 80;

    public float verticalSpeed = 150;
    public float horizontalSpeed = 300;

    private float angleX;
    private float angleY;

	void Start () {
	    angleX = -45;
        angleY = 0;
	}
	
	void Update () {
	    angleX += Input.GetAxis("Mouse Y") * Time.deltaTime * verticalSpeed;
        angleY += Input.GetAxis("Mouse X") * Time.deltaTime * verticalSpeed;

        angleX = Mathf.Clamp(angleX, minVerticalAngle, maxVerticalAngle);
        angleY %= 360;

        Quaternion xRotation = Quaternion.AngleAxis(angleX, new Vector3(1,0,0));
        Quaternion yRotation = Quaternion.AngleAxis(angleY, new Vector3(0,1,0));
        Vector3 offset = new Vector3(0,0,1);
        offset = xRotation * offset;
        offset = yRotation * offset;
        offset *= distance;

		offset = AddObstacleAvoidance (offset);

        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0,1,0));
	}

	Vector3 AddObstacleAvoidance(Vector3 targetToCamera){
		RaycastHit hit;
		if (Physics.Raycast (target.position, targetToCamera, out hit, distance, obstacleLayerMask)) {
            // if we hit an object between camera and target position the camera at (in front of) the hit object.
            return hit.point;
		}
		return targetToCamera;
	}
}
