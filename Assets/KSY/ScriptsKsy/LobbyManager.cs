using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbyManager : MonoBehaviour
{
    public TextMeshProUGUI[] lobbyOptions;
    public Image[] lobbyOpImage;
    public Camera mCam;
    public GameObject lfo;

    private int selectedIndex = 0;
    private Color dC;
    private Color sC = Color.white;

    public AudioClip selectSound;
    public AudioClip startSound;
    public AudioClip quitSound;
    public AudioClip noSound;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        dC = lobbyOptions[0].color;
    }

    void Update()
    {
        UpdateMenu();
        EventSystem.current.SetSelectedGameObject(lobbyOptions[selectedIndex].gameObject);
        EventSystem.current.SetSelectedGameObject(lobbyOpImage[selectedIndex].gameObject);
        HandleInput();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(4);
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex < lobbyOptions.Length - 1) ? selectedIndex + 1 : 0;
            selectedIndex = (selectedIndex < lobbyOpImage.Length - 1) ? selectedIndex + 1 : 0;
            AudioSource.PlayClipAtPoint(selectSound, mCam.transform.position);
            UpdateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : lobbyOptions.Length - 1;
            selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : lobbyOpImage.Length - 1;
            AudioSource.PlayClipAtPoint(selectSound, mCam.transform.position);
            UpdateMenu();

        }

        // 스페이스바로 선택
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectOption();
        }
    }

    public void UpdateMenu()
    {
        for (int i = 0; i < lobbyOptions.Length; i++)
        {
            if (i == selectedIndex)
            {
                lobbyOptions[i].color = sC;
                lobbyOpImage[i].enabled = true;
            }
            else
            {
                lobbyOptions[i].color = dC;
                lobbyOpImage[i].enabled = false;
            }
        }
    }

    private void SelectOption()
    {
        switch (selectedIndex)
        {
            case 0:
                AudioSource.PlayClipAtPoint(startSound, mCam.transform.position);
                lfo.GetComponent<LobbyFadeOut>().FadeOut();
                
                // 게임 시작
                break;
            case 1:
                AudioSource.PlayClipAtPoint(noSound, mCam.transform.position);
                // 추가 컨텐츠?
                break;
            case 2:
                AudioSource.PlayClipAtPoint(quitSound, mCam.transform.position);
                lfo.GetComponent<LobbyFadeOut>().FadeOutQuit();
                // 종료하기
                break;

        }
    }

}
