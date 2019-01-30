using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
