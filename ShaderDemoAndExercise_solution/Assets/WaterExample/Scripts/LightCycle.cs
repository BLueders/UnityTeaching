using UnityEngine;
using System.Collections;

public class LightCycle : MonoBehaviour {

    private Light light;
    public float rotationSpeed = 10f;

	void Awake () {
        light = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
        light.transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f), Space.World);
	}
}
