using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPlaySound : CollisionAction
{
    public AudioClip sound;

    protected override void DoAction()
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

    protected override void Start()
    {
        base.Start();
        if (sound == null)
        {
            Debug.LogError("OnCollisionPlaySound on object " + name + " requires audioclip assigned");
        }
    }
}
