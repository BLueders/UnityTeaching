using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
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
        if (Input.GetKey(KeyCode.UpArrow)) {
            movementVertical = speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movementVertical = -speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementHorizontal = speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementHorizontal = -speed;
        }
        rb2d.velocity = new Vector2(movementHorizontal, movementVertical);

        if (Input.GetKeyDown(KeyCode.RightShift)&& bp != null)
        {
            bp.PlaceBomb();
        }
    }
}
