using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public float horizontalSpeed;
    public float gravity;
    public float upForce;
    private float verticalSpeed = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 movement = new Vector3();
        movement.x = horizontalSpeed * Time.deltaTime;

        verticalSpeed -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) {
            verticalSpeed = upForce;
        }

        movement.y = verticalSpeed * Time.deltaTime;

        transform.position += movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Hornet>() != null) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
