using UnityEngine;
using System.Collections;

// replace this very simple camera logic with a better solution
public class CameraController : MonoBehaviour {

    public Transform target;

	void Start () {

	}
		
	void Update () {
		transform.position = target.position + new Vector3 (0, 3, -3);
		transform.LookAt (target);
	}
		
}
