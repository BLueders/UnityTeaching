using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionAction : MonoBehaviour
{
    public string objectTag = "Player";
    public bool onEnter = true;
    public bool onExit = false;
    public float secondsDelay = 0;

    protected virtual void Start()
    {
        Collider col = GetComponent<Collider>();
        Collider2D col2D = GetComponent<Collider2D>();
        if (col == null && col2D == null)
        {
            Debug.LogError("Collider or 2D Collider required on object " + name + " for script of type: " + this.GetType().Name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onEnter && other.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onEnter && other.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onEnter && collision.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onEnter && collision.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onExit && other.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (onExit && other.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (onExit && collision.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (onExit && collision.gameObject.CompareTag(objectTag))
        {
            StartCoroutine(ExecuteActionDelayed(DoAction, secondsDelay));
        }
    }

    private IEnumerator ExecuteActionDelayed(System.Action action, float delay)
    {
        if(delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        action();
        yield return null;
    }

    protected abstract void DoAction();
}
