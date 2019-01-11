using UnityEngine;
using System.Collections;

/// <summary>
/// Brain class for animating the brain. The collection logic is placed in Zombie.
/// </summary>
public class Brain : MonoBehaviour {

    void Update(){
        // Sine and Cosine functions are a nice way to create smooth animations in code
        // alternatively a real animation and animation controller can be created using the Mechanim system
        float scaleX = 1 + Mathf.Sin(Time.time * 5) * 0.1f;
        float scaleY = 1 + Mathf.Cos(Time.time * 5) * 0.1f;
        // due to the way the scale effects children and objects in the hiearchy, only the objects local scale can be changed on runtime
        transform.localScale = new Vector3(scaleX, scaleY , 1);
    }
}
