// <copyright file="CreateADie.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>08/01/2017</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to practice the handling of mesh information using the runtime creation of a cube mesh with a dice texture.
/// </summary>
public class CreateADie : MonoBehaviour
{
	public Material diceMaterial;

	MeshRenderer mr;
	MeshFilter mf;

	void Start ()
	{
		mf = gameObject.AddComponent<MeshFilter> ();
		mr = gameObject.AddComponent<MeshRenderer> ();
		CreateDie ();
	}

	private void CreateDie ()
	{

		// The meshs vertices. For a cube you would normally only need 8 vertices. But every vertex has a normal, and we need 3 normals per vertex to achieve a hard edge on a cube (one for each adjacent face). 
		// Thus there are 4 vertices per face = 4 * 6 faces = 3 * 8 vertices = 24.
		Vector3[] vertices = new Vector3[] {
			new Vector3 (0, 0, 0), new Vector3 (0, 1, 0), new Vector3 (1, 1, 0), new Vector3 (1, 0, 0),	//front
			new Vector3 (1, 0, 1), new Vector3 (1, 1, 1), new Vector3 (0, 1, 1), new Vector3 (0, 0, 1),	//back
			new Vector3 (1, 0, 0), new Vector3 (1, 1, 0), new Vector3 (1, 1, 1), new Vector3 (1, 0, 1),	//right
			new Vector3 (0, 0, 1), new Vector3 (0, 1, 1), new Vector3 (0, 1, 0), new Vector3 (0, 0, 0),	//left
			new Vector3 (0, 1, 0), new Vector3 (0, 1, 1), new Vector3 (1, 1, 1), new Vector3 (1, 1, 0),	//top
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), new Vector3 (1, 0, 1), new Vector3 (1, 0, 0)	//bottom
		}; 

		// On a cube every side has a quad, meaning 2 triangles leaving us with 2 * 6 = 12 triangles.
		// each triangle consists of 3 indices of its 3 vertices in the vertices array.
		int[] triangles = new int[] {
			0, 1, 2,		// front
			0, 2, 3,		// front
			4, 5, 6,		// back
			4, 6, 7,		// back
			8, 9, 10,		// right
			8, 10, 11,		// right
			12, 13, 14,		// left
			12, 14, 15,		// left
			16, 17, 18,		// top
			16, 18, 19,		// top
			20, 22, 21, 	// bottom
			20, 23, 22		// bottom
		};	

		// each vertex has normal information attached, so there are 24 normal entries for 24 vertices, always referring to the one with the same index.
		// change this so each triangle faces in the correct direction
		Vector3[] normals = new Vector3[] {
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0),	//front
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0),	//back
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0),	//right
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0),	//left
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0),	//top
			new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 0)	//bottom
		};

		// each vertex has uv information attached, so there are 24 uv entries for 24 vertices, always referring to the one with the same index.
		// change this so each triangle will display the correct part of the dice texture.
		Vector2[] uvs = new Vector2[] {
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0),	//front
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0),	//back
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0),	//right
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0),	//left
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0),	//top
			new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0)	//bottom
		};	

		// create the mesh, give it its vertices, triangles, uvs and normals.
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.normals = normals;

		// attach the data to the filter and renderer.
		mf.mesh = mesh;
		mr.material = diceMaterial;
	}
}
