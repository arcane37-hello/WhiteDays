using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isDragging = false; // 사다리를 드래그 중인지 여부
    private Vector3 offset; // 사다리와 마우스 사이의 거리
    private Camera mainCamera; // 메인 카메라 참조

    private void Start()
    {
        // 메인 카메라를 자동으로 할당
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned.");
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera == null) return;

        // 사다리에 마우스 클릭 시 드래그 시작
        isDragging = true;
        // 사다리의 현재 위치와 마우스 위치 사이의 오프셋을 계산
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // 사다리를 드래그 중일 때 마우스의 월드 위치로 사다리 위치를 업데이트
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        // 마우스 버튼을 떼면 드래그 종료
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) return Vector3.zero;

        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane; // 카메라의 near clip plane을 사용하여 Z 축 값 설정
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 사다리의 속도를 감소시키거나 처리
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // 충돌 시 속도를 0으로 설정
        }
    }
}