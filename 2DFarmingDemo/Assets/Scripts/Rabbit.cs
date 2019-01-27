using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal{

	public GameObject rabbitPrefab;

	public override void Feed(){

		Vector3 target = transform.position;
		target.x += Random.Range(-1f,1f);
		target.y += Random.Range(-1f,1f);

		Instantiate(rabbitPrefab, target, Quaternion.identity);

	}
}
