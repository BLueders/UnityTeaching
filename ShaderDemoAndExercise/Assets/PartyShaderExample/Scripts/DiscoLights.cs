using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {

    public Light[] lights;
    public float flicker = 3;
    public float movement = 3;


    float[] switchTimers;
    float[] rotationTimers;

    Quaternion[] startRotations;
    Quaternion[] targetRotations;

    void Start(){
        switchTimers = new float[lights.Length];
        rotationTimers = new float[lights.Length];
        startRotations = new Quaternion[lights.Length];
        targetRotations = new Quaternion[lights.Length];

        for(int i = 0; i < lights.Length; i++){
            switchTimers[i] = Random.Range(0.1f, flicker);
            rotationTimers[i] = Random.Range(0.1f, movement);
            lights[i].gameObject.SetActive(Random.Range(0,1) == 0 ? true : false);
            startRotations[i] = lights[i].transform.rotation;
            targetRotations[i] = startRotations[i] * Quaternion.Euler(Random.Range(-45,45), Random.Range(-45,45), Random.Range(-45,45));
        }
    }

    void Update(){
        for(int i = 0; i < lights.Length; i++){
            switchTimers[i] -= Time.deltaTime;
            if(switchTimers[i] < 0){
                lights[i].gameObject.SetActive(!lights[i].gameObject.activeSelf);
                switchTimers[i] = Random.Range(0.1f, flicker);
            }

            rotationTimers[i] -= Time.deltaTime;
            if(rotationTimers[i] < 0){
                rotationTimers[i] = Random.Range(0.1f, movement);
                targetRotations[i] = startRotations[i] * Quaternion.Euler(Random.Range(-45,45), Random.Range(-45,45), Random.Range(-45,45));
            }
            lights[i].transform.rotation = Quaternion.Lerp(lights[i].transform.rotation, targetRotations[i], Time.deltaTime * 1.5f); 
        }
    }
}
