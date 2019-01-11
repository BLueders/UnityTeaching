// <copyright file="PlayerController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>05/10/2017</date>

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Camera cam;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        // if we are facing somewhat from the side, use forward vector as direction, 
        // if we are facing pretty straight from the top use up vector as direction
        // this avoids errors when normalizing a zero vector (up vector projected on XZ plane is a zero vector)
        Vector3 xzProjection = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
        if(xzProjection.magnitude < 0.05f){
            xzProjection = Vector3.ProjectOnPlane(cam.transform.up, Vector3.up);
        }
        Vector3 forward = xzProjection.normalized;

        Vector3 movement = forward * moveVertical + cam.transform.right * moveHorizontal;

        rb.AddForce (movement * speed);
    }
}