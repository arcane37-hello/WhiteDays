using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button startBtn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
