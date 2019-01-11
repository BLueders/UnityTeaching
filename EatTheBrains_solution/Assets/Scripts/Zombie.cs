using UnityEngine;
using System.Collections;

/// <summary>
/// Zombie class, handles controls and eating of the brains
/// </summary>
public class Zombie : MonoBehaviour {

    public float speed = 5;
    int score;

	AudioSource audioSource;
    BrainSpawn brainSpawn;

    void Start() {
		audioSource = GetComponent<AudioSource> ();
        brainSpawn = GameObject.FindWithTag("BrainSpawn").GetComponent<BrainSpawn>();
    }

    void Update(){
        // This is a little help to get the input. Input.GetAxis receives input data from
        // any Keyboard (w,a,s,d or arrow keys) or Joystick/Gamepad attached.
        // The input data is between -1 and 1. For arrow keys that means down arrow pressed 
        // resolves in inputY = -1 and up arrow pressed resolves in inputY = 1.
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Get direction from input
        Vector3 direction = new Vector3(inputX, inputY, 0);
        // Normalize direction so we do not move faster diagonally
        if(direction.magnitude > 1) {
            direction.Normalize();
        }
        // Calculate framerate independent movement vector using speed and deltatime
        Vector3 movement = direction * speed * Time.deltaTime;
        // Move!
        transform.position += movement;

        // If we have input, rotate
        if(inputX != 0 || inputY != 0) {
            // Get rotation angle using Atan2
            float angle = Mathf.Atan2(inputY, inputX);
            // Create rotation around forward axis, sprite is facing upwards per default, so substract 90 degrees (Atan2 angle 0 is in x direction or right)
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90, Vector3.forward);
        }
    }

    // If we touch a brain, eat it and tell brain spawn that there is one brain less on the field
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Brain"){
			audioSource.Play ();
            Destroy(other.gameObject);
            score++;
            brainSpawn.OnCollectBrain();
        }
    }
}
