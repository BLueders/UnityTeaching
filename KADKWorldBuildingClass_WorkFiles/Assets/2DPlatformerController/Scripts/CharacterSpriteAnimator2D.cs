using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController2D))]
public class CharacterSpriteAnimator2D : MonoBehaviour
{
    [SerializeField] Sprite[] walkCycle;
    [SerializeField] Sprite[] idleCycle;
    [SerializeField] Sprite[] airCycle;

    [SerializeField] float walkAnimationSpeed = 5;
    [SerializeField] float idleAnimationSpeed = 5;
    [SerializeField] float airAnimationSpeed = 5;

    PlatformerController2D platformerController;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    bool isWalking;
    float timer;
    int facing = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on animated object: " + gameObject.name + " or its children");
        }
        platformerController = GetComponent<PlatformerController2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalSpeed = Mathf.Abs(rb2d.velocity.x);

        if (rb2d.velocity.x > 0 && facing == -1)
        {
            facing = 1;
        }
        else if (rb2d.velocity.x < 0 && facing == 1)
        {
            facing = -1;
        }
        spriteRenderer.flipX = facing == -1;

        Sprite[] currentCycle;
        if (platformerController.grounded)
        {
            if(horizontalSpeed > 0.5f)
            {
                currentCycle = walkCycle;
                timer += Time.deltaTime * walkAnimationSpeed;
            }
            else
            {
                currentCycle = idleCycle;
                timer += Time.deltaTime * idleAnimationSpeed;
            }
        }
        else
        {
            currentCycle = airCycle;
            timer += Time.deltaTime * airAnimationSpeed;
        }

        if (timer > currentCycle.Length)
        {
            timer = 0;
        }
        spriteRenderer.sprite = currentCycle[(int)(timer)];
    }
}
