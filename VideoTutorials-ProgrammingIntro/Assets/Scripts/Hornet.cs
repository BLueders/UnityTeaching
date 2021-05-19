using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : MonoBehaviour
{
    public float speed;
    public float range;
    private Vector3 startPostition;
    private int direction = 1;

    void Start()
    {
        startPostition = transform.position;
    }

    void Update()
    {
        if(transform.position.y > startPostition.y + range) {
            direction = -1;
        }
        if (transform.position.y < startPostition.y - range) {
            direction = 1;
        }
        transform.position += new Vector3(0.0f, speed * Time.deltaTime * direction, 0.0f);
    }
}
