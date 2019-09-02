using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionRestartScene : CollisionAction
{
    protected override void DoAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void Start()
    {
        base.Start();
    }
}
