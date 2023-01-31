using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float speedX = Input.GetAxis("Horizontal") * speed;
        float speedY = Input.GetAxis("Vertical") * speed;

        rb2D.velocity = new Vector2(speedX, speedY);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Animal animal = other.gameObject.GetComponent<Animal>();
    //    if(animal != null)
    //    {
    //        animal.Feed();
    //    }
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    //void Update()
    //{
    //    float movementHorizontal = 0;
    //    float movementVertical = 0;
    //    if (Input.GetKey(KeyCode.UpArrow)) {
    //        movementVertical = speed;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        movementVertical = -speed;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        movementHorizontal = speed;
    //    }
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        movementHorizontal = -speed;
    //    }
    //    rb2d.velocity = new Vector2(movementHorizontal, movementVertical);
    //}
}
