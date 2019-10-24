using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal {
    
	public GameObject rabbitPrefab;

	public override void Feed(){

        Vector3 offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Vector3 pos = transform.position + offset;
        Instantiate(rabbitPrefab, pos, Quaternion.identity);
	}
}
