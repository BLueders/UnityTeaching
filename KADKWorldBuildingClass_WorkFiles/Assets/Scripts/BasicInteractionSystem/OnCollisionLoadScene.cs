using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionLoadScene : CollisionAction
{
    public string sceneName;

    protected override void DoAction()
    {
        SceneManager.LoadScene(sceneName);
    }

    protected override void Start()
    {
        base.Start();
    }
}
