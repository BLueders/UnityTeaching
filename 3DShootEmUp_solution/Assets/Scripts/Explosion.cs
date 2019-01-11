using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the explosion animation on the explosion GameObject. 
/// </summary>
public class Explosion : MonoBehaviour
{
    public AudioClip explosionSound;

    /// <summary>
    /// Start is called by Unity. This will play our explosion sound 
    /// </summary>
    void Start()
    {
        if (explosionSound != null) {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        StartCoroutine(DestroyAfterSeconds(1));
    }

    /// <summary>
    /// This is a coroutine waits "time" then destorys the gameobject.
    /// </summary>
    IEnumerator DestroyAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
