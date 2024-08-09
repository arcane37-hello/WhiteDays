using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SMSSc : MonoBehaviour
{
    public Camera itemCamera;
    public RenderTexture renderTexture;
    public GameObject sms;

    void Start()
    {

    }

    public void ShowItem(GameObject itemPrefab)
    {
        if (sms != null)
        {
            Destroy(sms);
        }
        sms = Instantiate(itemPrefab);
        itemCamera.targetTexture = renderTexture;
        PositionItemInFrontOfCamera();
    }

    private void PositionItemInFrontOfCamera()
    {
        // 카메라가 아이템을 중앙에서 비추도록 위치 조정
        itemCamera.transform.position = new Vector3(0, 0, -5);
        itemCamera.transform.LookAt(sms.transform);
    }

    void Update()
    {
    }
    void OnDisable()
    {
        // 아이템을 파괴하여 메모리 해제
        if (sms != null)
        {
            Destroy(sms);
        }
        itemCamera.targetTexture = null;
    }
}
