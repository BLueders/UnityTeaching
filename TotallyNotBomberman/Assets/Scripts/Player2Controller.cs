using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb2d;

    BombPlacement bp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bp = GetComponent<BombPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementHorizontal = 0;
        float movementVertical = 0;
        if (Input.GetKey(KeyCode.W)) {
            movementVertical = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVertical = -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementHorizontal = speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementHorizontal = -speed;
        }
        rb2d.velocity = new Vector2(movementHorizontal, movementVertical);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            bp.PlaceBomb();
        }
    }
}
