using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float interactionRange = 3.0f;  // 상호작용 거리
    public Camera playerCamera;            // 플레이어의 카메라

    private void Start()
    {
        // 플레이어의 카메라가 설정되지 않은 경우, 기본 카메라를 자동으로 할당
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.LogError("Player camera is not assigned and no main camera found.");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned.");
            return;
        }

        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Raycast 경로를 디버깅으로 시각화
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Debug.Log("Raycast hit: " + hit.transform.name); // Raycast 충돌 확인

            if (hit.collider.CompareTag("Door"))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.Move(); // 문을 이동시킵니다
                    Debug.Log("Door component found and moving door.");
                }
                else
                {
                    Debug.Log("No Door component found on the hit object.");
                }
            }
            else
            {
                Debug.Log("Raycast hit object does not have the 'door' tag.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object within interaction range.");
        }
    }
}