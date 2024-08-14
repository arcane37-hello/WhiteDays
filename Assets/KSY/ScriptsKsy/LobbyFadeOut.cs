using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyFadeOut : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float fadedTime = 0.8f;

    public GameObject panel;
    public AudioSource bgm;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void FadeOut()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeOutStart());
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


    IEnumerator CoFadeOutStart()
    {
        float volume = bgm.volume;
        while(elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            while(volume > 0)
            {
                volume -= 0.3f;
                bgm.volume = volume;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
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
        //Ãß°¡ ÄÁÅÙÃ÷??
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
