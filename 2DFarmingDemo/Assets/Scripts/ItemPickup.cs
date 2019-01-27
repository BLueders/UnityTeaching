using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	GameObject pickedItem;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Carrot") && pickedItem == null){
			other.transform.SetParent(transform);
			other.transform.position = transform.position + Vector3.right * 0.5f;
			pickedItem = other.gameObject;
		}

		else if(other.CompareTag("Animal") && pickedItem != null){
			Animal animal = other.GetComponent<Animal>();
			if(animal != null){
				animal.Feed();
				Destroy(pickedItem);
			}
		}
	}
}
