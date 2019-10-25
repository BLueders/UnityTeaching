using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public enum Status
    {
        Active,
        Inactive
    }

    [Header("Animations")]
    [SerializeField] GameObject UIPanel = null;
    [SerializeField] float inOutTime = 0.5f;

    Status status;

    void Start()
    {
        status = Status.Inactive;
        Time.timeScale = 1;
        UIPanel.SetActive(false);
        UIPanel.transform.localScale = new Vector3(0, 1, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            if (status == Status.Active)
            {
                Time.timeScale = 1;
                StartCoroutine(CloseRoutine());
            }
            else if (status == Status.Inactive)
            {
                Time.timeScale = 0;
                StartCoroutine(OpenRoutine());
            }
        }
    }

    IEnumerator OpenRoutine()
    {
        status = Status.Active;
        UIPanel.SetActive(true);
        if (JuiceControl.PauseMenuFadeIn)
        {
            float timer = 0;
            while (timer < inOutTime)
            {
                timer += Time.unscaledDeltaTime;
                float normalizedTime = timer / inOutTime;
                UIPanel.transform.localScale = new Vector3(Easing.Cubic.In(normalizedTime), 1, 1);
                yield return null;
            }
        }
        UIPanel.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator CloseRoutine()
    {
        if (JuiceControl.PauseMenuFadeIn)
        {
            float timer = 0;
            while (timer < inOutTime)
            {
                timer += Time.unscaledDeltaTime;
                float normalizedTime = timer / inOutTime;
                UIPanel.transform.localScale = new Vector3(Easing.Cubic.In(1 - normalizedTime), 1, 1);
                yield return null;
            }
        }
        UIPanel.transform.localScale = new Vector3(0, 1, 1);
        status = Status.Inactive;
        UIPanel.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Close()
    {
        Time.timeScale = 1;
        StartCoroutine(CloseRoutine());
    }
}
