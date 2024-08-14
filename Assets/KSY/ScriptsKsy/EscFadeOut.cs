using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscFadeOut : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float fadedTime = 0.8f;
    public GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void FadeOutOther()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeOutOther());
    }

    public void FadeOutQuit()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeOutQuit());
    }




    IEnumerator CoFadeOutOther()
    {
        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    IEnumerator CoFadeOutQuit()
    {
        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
