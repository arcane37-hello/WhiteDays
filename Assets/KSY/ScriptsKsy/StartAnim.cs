using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartAnim : MonoBehaviour
{
    public GameObject panel;
    public VideoPlayer videoPlayer;
    void Start()
    {
        if(videoPlayer == null)
        {
            Debug.LogError("비디오 플레이어가 할당되지 않음");
            return;
        }
        panel.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnded;
        videoPlayer.Play();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            panel.SetActive(true);
            GameStart();
        }
    }


    private void OnVideoEnded(VideoPlayer vp)
    {
        panel.SetActive(true);
        GameStart();
    }

    public void GameStart()
    {
        panel.SetActive(true );
        StartCoroutine(FadeIN());
    }

    IEnumerator FadeIN()
    {
        float elapesdTime = 0f;
        float fadedTime = 2f;
        while(elapesdTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapesdTime / fadedTime));
            elapesdTime += Time.deltaTime;
            yield return null;
        }
        panel.SetActive(true );
        SceneManager.LoadScene(1);
    }
}
