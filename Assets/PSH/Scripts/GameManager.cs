using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject gameOverUI;


    void Awake()
    {
        // 씬에 단 한 개만 존재하도록 처리
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void showGameOverUI()
    {
        // 게임 오버 UI 오브젝트를 활성화한다.
        gameOverUI.SetActive(true);

        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
        Time.timeScale = 0;
    }
}
