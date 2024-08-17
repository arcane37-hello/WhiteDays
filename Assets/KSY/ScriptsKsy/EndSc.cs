using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSc : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float duration = 2;
    private bool isShowingRandomTime = true;
    public string finalTime;
    public AudioClip dududu;
    public AudioClip end;
    public Camera mcam;

    void Start()
    {
        finalTime = GameManager.GetPlayTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator DisplayRandomTime()
    {
        float startTime = Time.time;
        while(Time.time < startTime + duration)
        {
            if(isShowingRandomTime)
            {
                timeText.text = GRTime();
                AudioSource.PlayClipAtPoint(dududu, mcam.transform.position, 0.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        timeText.text = finalTime;
        AudioSource.PlayClipAtPoint(end, mcam.transform.position);
    }
    

    string GRTime()
    {
        int hour = Random.Range(0, 24);
        int minute = Random.Range(0, 60);
        int second = Random.Range(0, 60);
        int mS = Random.Range(0, 1000);

        return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", hour, minute, second, mS);
    }
}
