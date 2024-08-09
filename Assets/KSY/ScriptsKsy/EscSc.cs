using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EscSc : MonoBehaviour
{
    public GameObject player;
    public GameObject dalsu;
    public GameObject mm;
    private bool isPaused = false;
    private bool isChasing;

    public Canvas Esc;

    public TextMeshProUGUI ingontext;
    public TextMeshProUGUI setontext;
    public TextMeshProUGUI quitontext;

    public TextMeshProUGUI ingofftext;
    public TextMeshProUGUI setofftext;
    public TextMeshProUGUI quitofftext;

    public Canvas cuIn;
    

    void Start()
    {
        Esc.enabled = false;
    }

    void Update()
    {
        isChasing = dalsu.GetComponent<DalsuMove>().isChase;
        cuIn = mm.GetComponent<MMSc>().currentInventory;
        if (Input.GetKeyDown(KeyCode.Escape) && !isChasing)
        {
            ToggleEsc();
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            CloseEsc();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isChasing)
        {
        }
    }

    void ToggleEsc()
    {
        ToggleEscUI();
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; 
    }

    void ToggleEscUI()
    {
        if (Esc.enabled == true)
        {
            CloseEsc();
        }
        else
        {
            if(cuIn != null)
            {
                cuIn.enabled = false;
            }
            Esc.enabled = true;
            Time.timeScale = 0f;
        }
    }

    void CloseEsc()
    {
        if(Esc!=null)
        {
            Esc.enabled = false;
            Time.timeScale = 1f;
        }
    }


}
