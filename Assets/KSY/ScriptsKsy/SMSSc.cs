using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;

public class SMSSc : MonoBehaviour
{
    public Canvas smsC;
    public MMSc mm;
    
    public GameObject sms;
    public RawImage rawImage;
    public Camera smsCamera;

    void Start()
    {
    }

    public void SMSC()
    {
        mm.icam.depth = 30;
        mm.icam = smsCamera;
        mm.icam.depth = 50;
    }


}
