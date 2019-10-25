using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    private void Start()
    {

    }

    public static void Wait()
    {
        StartCoroutine(WaitRoutine());
    }

    public IEnumerator WaitRoutine()
    {
        yield return new WaitForEndOfFrame();
    }
}
