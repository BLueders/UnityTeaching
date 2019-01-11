/*
 * Created on Mon Jun 18 2018
 *
 * Copyright (c) 2018 DIS
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller class for the camera, moves it along with the chicken
/// </summary>
public class CameraController : MonoBehaviour {

	public Transform player;

	public float offsetX;
	
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(transform.position.x - player.position.x < offsetX){
			Vector3 pos = transform.position;
			pos.x = player.position.x + offsetX;
			transform.position = pos;
		}
	}
}
