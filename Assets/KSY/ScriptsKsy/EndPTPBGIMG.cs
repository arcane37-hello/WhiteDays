using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndPTPBGIMG : MonoBehaviour
{
    public Image ptpBG;
    public float blinkSpeed = 0.6f;

    private bool isFade = true;
    void Start()
    {
        if (ptpBG == null)
        {
            ptpBG = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color cC = ptpBG.color;
        float tA = blinkSpeed * Time.deltaTime;
        if (isFade)
        {
            cC.a -= tA;
            if (cC.a <= 0)
            {
                cC.a = 0;
                isFade = false;
            }
        }
        else
        {
            cC.a += tA;
            if (cC.a >= 0.5)
            {
                cC.a = 0.5f;
                isFade = true;
            }
        }
        ptpBG.color = cC;
    }
}
