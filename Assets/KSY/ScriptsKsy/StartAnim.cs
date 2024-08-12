using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartAnim : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    void Start()
    {
        if(videoPlayer == null)
        {
            Debug.LogError("비디오 플레이어가 할당되지 않음");
            return;
        }
        videoPlayer.loopPointReached += OnVideoEnded;
        videoPlayer.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }
}
