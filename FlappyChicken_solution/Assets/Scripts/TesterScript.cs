using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterScript : MonoBehaviour
{
    public float speed;
    public Sprite newSprite;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int test = 0;
        test++;
        test++;
        test++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= 5 )
        {
            sr.sprite = newSprite;
        }

        float movement = speed * Time.deltaTime; // (1/fps)
        //transform.position += new Vector3(movement, 0, 0);
    }
}
