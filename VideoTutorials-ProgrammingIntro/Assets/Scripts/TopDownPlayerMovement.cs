using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public int score = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1.0f) {
            input.Normalize();
        }
        rb2d.velocity = input * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin")) {
            score++;
        }
        if (other.gameObject.CompareTag("badcoin")) {
            score--;
        }
        Destroy(other.gameObject);
    }
}
