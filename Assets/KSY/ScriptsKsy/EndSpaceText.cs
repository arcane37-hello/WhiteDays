using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSpaceText : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public float blinkSpeed = 1f;
    private bool isFade = true;
    void Start()
    {
        if (tm == null)
        {
            tm = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        Color cC = tm.color;
        float tA = blinkSpeed * Time.deltaTime;
        if(isFade)
        {
            cC.a -= tA;
            if(cC.a <= 0)
            {
                cC.a = 0;
                isFade = false;
            }
        }
        else
        {
            cC.a += tA;
            if(cC.a >= 1)
            {
                cC.a = 1;
                isFade = true;
            }
        }
        tm.color = cC;
    }
}
