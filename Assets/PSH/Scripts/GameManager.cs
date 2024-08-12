using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject gameOverUI;

    private float startTime;
    private string timeString;


    void Awake()
    {
        // 씬에 단 한 개만 존재하도록 처리
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 1)
        {
            startTime = Time.time;
        }
        else if(currentSceneIndex == 3)
        {
            timeString = PlayerPrefs.GetString("PlayTime", "00:00:00:000");
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            float playTime = Time.time - startTime;
            int hour = (int)(playTime / 3600);
            int minute = (int)((playTime % 3600) / 60);
            int second = (int)(playTime % 60);
            int millisecond = (int)((playTime * 1000) % 1000);
            timeString = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", hour, minute, second, millisecond);
        }
    }

    public void showGameOverUI()
    {
        // 게임 오버 UI 오브젝트를 활성화한다.
        gameOverUI.SetActive(true);

        // 경과 시간을 저장 (씬 전환 후에도 사용 가능)
        PlayerPrefs.SetString("PlayTime", timeString);

        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
        Time.timeScale = 0;
    }

    public void GameClear()
    {
        // 경과 시간을 저장 (씬 전환 후에도 사용 가능)
        PlayerPrefs.SetString("PlayTime", timeString);

        // 현재는 크레딧 씬이 아니라 바로 엔딩 씬으로 건너 뜁니다. 
        SceneManager.LoadScene(3);
    }

    public static string GetPlayTime()
    {
        // 저장된 경과 시간을 불러온다.
        return gm.timeString;
    }
}
