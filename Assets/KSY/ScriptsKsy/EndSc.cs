using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSc : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        string finalTime = GameManager.GetPlayTime();
        timeText.text = finalTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
    
}
