// <copyright file="Soldier.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the soldier that shoots the balloons.
/// A laser is cast from the LaserStart object to where the mouse position on the screen is when the left mouse button is pressed.
/// Use a RayCast to check if the laser hits any balloons.
/// </summary>
public class Soldier : MonoBehaviour {

	public GameObject laserStart;
	public GameObject crosshair;
	public LineRenderer laserLineRenderer;

	public LayerMask balloonLayerMask;

	void Update () {

        UpdateCrosshair(GetMouseWorldPosition());
		OrientSoldier ();
		laserLineRenderer.enabled = false;
		if (Input.GetMouseButton (0)) {
			ImmaFirinMaLazor ();
			laserLineRenderer.enabled = true;
		}
	}

	/// <summary>
	/// Orients the soldier using the mouse position (crosshair)
	/// </summary>
	void OrientSoldier(){
		// get the direction in 3D, normalize and use the direction with Atan2 to calculate the angle 
		Vector3 dir3D = crosshair.transform.position - transform.position;
		dir3D.Normalize ();
		float angle = Mathf.Atan2 (dir3D.y, dir3D.x) * Mathf.Rad2Deg;
		angle -= 90; // since the sprite is facing upwards
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

    /// <summary>
    /// Grabs the world position of the mouse with z = 0.
    /// </summary>
    /// <returns>World position of mouse as Vector3</returns>
    Vector3 GetMouseWorldPosition()
    {
        // this gets the current mouse position (in screen coordinates) and transforms it into world coordinates
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // the camera is on z = -10, so all screen coordinates are on z = -10. To be on the same plane as the game, we need to set z to 0
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }

    /// <summary>
    /// Updates the crosshair position and the line renderer from the laser to point from laserStart to the crosshair
    /// </summary>
    void UpdateCrosshair(Vector3 newCrosshairPosition)
    {
        crosshair.transform.position = newCrosshairPosition;
        laserLineRenderer.SetPosition(0, laserStart.transform.position);
        laserLineRenderer.SetPosition(1, crosshair.transform.position);
    }

    /// <summary>
    /// Fire tha lazorz! Whoooooaaaaaaaaaa
    /// </summary>
    void ImmaFirinMaLazor(){
		// get the direction in 2D from the laser start to target and do the raycast
		Vector3 dir3D = crosshair.transform.position - laserStart.transform.position;
		Vector2 dir2D = new Vector2 (dir3D.x, dir3D.y);
		float length = dir2D.magnitude;
		dir2D.Normalize ();
		RaycastHit2D hit = Physics2D.Raycast(laserStart.transform.position, dir2D, length, balloonLayerMask);
		if (hit.collider != null) {
			if (hit.collider.CompareTag ("Balloon")) {
				hit.collider.GetComponent<Balloon> ().Death ();
			}
		}
	}
}
