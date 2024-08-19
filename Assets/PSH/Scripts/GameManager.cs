using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject gameOverUI;
    public Canvas gameOVUI;

    private float startTime;
    private string timeString;


    void Awake()
    {
        // ���� �� �� ���� �����ϵ��� ó��
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� GameManager�� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameOVUI.enabled = false;
        // gameOverUI.SetActive = false;
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
        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
        // gameOverUI.SetActive(true);
        gameOVUI.enabled = true;
          
        // ��� �ð��� ���� (�� ��ȯ �Ŀ��� ��� ����)
        PlayerPrefs.SetString("PlayTime", timeString);
        

        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� �����)
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
        
    }

    public void GameClear()
    {
        // ��� �ð��� ���� (�� ��ȯ �Ŀ��� ��� ����)
        PlayerPrefs.SetString("PlayTime", timeString);

        // ����� ũ���� ���� �ƴ϶� �ٷ� ���� ������ �ǳ� �ݴϴ�. 
        SceneManager.LoadScene(3);
    }

    public static string GetPlayTime()
    {
        // ����� ��� �ð��� �ҷ��´�.
        return gm.timeString;
    }
}
