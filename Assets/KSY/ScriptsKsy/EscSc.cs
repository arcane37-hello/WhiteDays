using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscSc : MonoBehaviour
{
    public GameObject player;
    public GameObject dalsu;
    public GameObject dalsu2;
    public GameObject mm;
    public GameObject escF;
    private bool isChasing;
    public PlayerMove pm;

    public Canvas escCanvas;

    public TextMeshProUGUI[] menuOptions;
    public Image[] menuOpImage;
    public Camera mcam;

    public AudioClip selectSound;
    public AudioClip startSound;
    public AudioClip quitSound;
    private int selectedIndex = 0;

    private Canvas cuIn;

    private Color dC;
    private Color sC = Color.yellow;
    private Vector2[] dP;
    

    void Start()
    {
        escCanvas.enabled = false;
        dC = menuOptions[0].color;
        dP = new Vector2[menuOptions.Length];
        for(int i = 0; i < menuOptions.Length; i++)
        {
            dP[i] = menuOptions[i].rectTransform.anchoredPosition;
        }
        UpdateMenu();
    }

    void Update()
    {
        cuIn = mm.GetComponent<MMSc>().currentInventory;
        if (dalsu != null)
        {
            isChasing = dalsu.GetComponent<DalsuMove>().isChase;
        }
        else if(dalsu == null)
        {
            isChasing = dalsu2.GetComponent<DalsuMove>().isChase;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isChasing)
        {
            if (escCanvas.enabled == true)
            {
                escCanvas.enabled = false;
                pm.canMove = true;
                pm.canRot = true;
                mm.GetComponent<MMSc>().CloseMenu();
            }
            else
            {
                if (cuIn != null && cuIn.enabled == true)
                {
                    cuIn.enabled=false;
                }
                pm.canMove = false;
                pm.canRot = false;
                mm.GetComponent<MMSc>().OpenMenu();
                escCanvas.enabled = true;
                selectedIndex = 0;
                UpdateMenu();
                EventSystem.current.SetSelectedGameObject(menuOptions[selectedIndex].gameObject);
            }
        }

        if(escCanvas.enabled == true)
        {
            HandleInput();
        }

    }


    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex > 0) ? selectedIndex : menuOptions.Length - 1;
            selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : menuOpImage.Length - 1;
            UpdateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex < menuOptions.Length - 1) ? selectedIndex : 0;
            selectedIndex = (selectedIndex < menuOpImage.Length - 1) ? selectedIndex + 1 : 0;
            UpdateMenu();
        }

        // �����̽��ٷ� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectOption();
        }
    }

    private void UpdateMenu()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == selectedIndex)
            {
                // ���õ� ������ �ð��� ȿ��
                menuOptions[i].rectTransform.anchoredPosition = dP[i] + new Vector2(20, 0);
                menuOptions[i].color = sC;
                menuOpImage[i].enabled = true;
            }
            else
            {
                menuOptions[i].rectTransform.anchoredPosition = dP[i];
                menuOptions[i].color = dC;
                menuOpImage[i].enabled = false;
            }
        }
    }
    public void SelectOption()
    {
        switch (selectedIndex)
        {
            case 0:
                // ���� �̾��ϱ�
                AudioSource.PlayClipAtPoint(startSound, mcam.transform.position);
                escCanvas.gameObject.SetActive(false);
                break;
            case 1:
                // �κ�� ���ư���
                AudioSource.PlayClipAtPoint(quitSound, mcam.transform.position);
                escF.GetComponent<EscFadeOut>().FadeOutOther();
                break;
            case 2:
                // �����ϱ�
                AudioSource.PlayClipAtPoint(quitSound, mcam.transform.position);
                escF.GetComponent<EscFadeOut>().FadeOutQuit();
                break;
            
        }
    }
}
