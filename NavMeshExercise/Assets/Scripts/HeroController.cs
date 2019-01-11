using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class HeroController : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;

	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	void Update () {
		// This will set the agents destination to where the mouse points
		if (Input.GetMouseButton(0)){
			RaycastHit rh = new RaycastHit ();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out rh, float.MaxValue)) {
				Vector3 hitPosition = rh.point;
				agent.destination = hitPosition;
			}
		}
	}
}
