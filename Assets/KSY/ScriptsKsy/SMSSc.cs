using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SMSSc : MonoBehaviour
{
    public Canvas smsC;
    
    public GameObject sms;
    public Vector3 camOs = new Vector3 (0, 0, -100);
    public Vector2 renderTextureSize = new Vector2(800, 800);
    public float camDisF = 1.5f;
    public RawImage rawImage;
    public LayerMask smsLayer;

    private Camera renderCamera;
    private RenderTexture renderTexture;


    void Start()
    {
        if(sms == null)
        {
            Debug.LogError("문자 프리팹 혹은 캔버스가 없음");
            return;
        }
        if (rawImage == null)
        {
            Debug.LogError("RawImage가 설정되지 않았습니다. Inspector에서 RawImage를 연결하세요.");
            return;
        }
    }

    public void SMSC()
    {
        //SetupCamera();
        //SetupRenderTexture();
        //SetupRawImage();
        //PositionCamera();
    }


    public void SetupCamera()
    {
        GameObject cameraObject = new GameObject("RenderCamera");
        renderCamera = cameraObject.AddComponent<Camera>();
        renderCamera.clearFlags = CameraClearFlags.SolidColor;
        renderCamera.backgroundColor = Color.clear;
        renderCamera.orthographic = false;
        renderCamera.cullingMask = smsLayer;
    }

    public void SetupRenderTexture()
    {
        renderTexture = new RenderTexture((int)renderTextureSize.x, (int)renderTextureSize.y, 24);
        renderCamera.targetTexture = renderTexture;
    }

    public void SetupRawImage()
    {
        rawImage.texture = renderTexture;
        rawImage.rectTransform.sizeDelta = renderTextureSize;
    }

    public void PositionCamera()
    {
        Bounds objectBounds = CalculateBounds(sms);
        float objectSize = Mathf.Max(objectBounds.size.x, objectBounds.size.y, objectBounds.size.z);
        float camDis = objectSize * camDisF;
        renderCamera.transform.position = objectBounds.center + camOs.normalized * camDis;
        renderCamera.transform.LookAt(objectBounds.center);
    }

    public Bounds CalculateBounds(GameObject obj)
    {
        Renderer[] rendereres = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);

        foreach(Renderer renderere in rendereres)
        {
            bounds.Encapsulate(renderere.bounds);
        }
        return bounds;
    }

    void Update()
    {
        if (smsC != null && !smsC.enabled)
        {
            DestroyResources();
        }
    }
    private void DestroyResources()
    {
        if (renderCamera != null)
        {
            Destroy(renderCamera.gameObject);
        }

        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }

        rawImage.texture = null;

    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
