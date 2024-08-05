using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    public float interactionRange = 3.0f; // 열쇠와의 상호작용 거리
    public Camera playerCamera; // 플레이어의 카메라

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // 기본 카메라를 자동으로 할당
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryCollectKey();
        }
    }

    void TryCollectKey()
    {
        RaycastHit hit;

        // 카메라의 정면으로 Raycast를 쏘아서 열쇠를 감지합니다.
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            Key key = hit.collider.GetComponent<Key>();

            if (key != null)
            {
                key.Collect(); // 열쇠를 수집합니다.
            }
        }
    }
}