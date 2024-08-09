using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public float rotationAngle = 90f; // 회전할 각도 (도 단위)
    public float rotationDuration = 1f; // 회전할 시간 (초)

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private bool rotatingToTarget = false;
    private float rotationStartTime;

    private PlayerInventory playerInventory; // 플레이어의 인벤토리 상태를 참조할 변수

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation * Quaternion.Euler(0, rotationAngle, 0);

        // 플레이어의 PlayerInventory 컴포넌트를 찾아서 할당합니다.
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        // 마우스 왼쪽 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인
                if (hit.transform == transform)
                {
                    // 플레이어가 열쇠를 가지고 있는지 확인
                    if (playerInventory != null && playerInventory.hasKey)
                    {
                        if (!isRotating)
                        {
                            if (rotatingToTarget)
                            {
                                // 원래 회전 상태로 돌아가도록
                                StopAllCoroutines();
                                StartCoroutine(RotateToOriginal());
                            }
                            else
                            {
                                // 목표 회전으로 회전하도록
                                StartCoroutine(RotateToTarget());
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("You need a key to open this door.");
                    }
                }
            }
        }

        // 회전 중에는 부드럽게 회전하도록 처리
        if (isRotating)
        {
            float elapsedTime = Time.time - rotationStartTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            Quaternion startRotation = rotatingToTarget ? originalRotation : targetRotation;
            Quaternion endRotation = rotatingToTarget ? targetRotation : originalRotation;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 회전 완료 체크
            if (t >= 1f)
            {
                isRotating = false;
                rotatingToTarget = !rotatingToTarget; // 회전 방향 전환
            }
        }
    }

    private System.Collections.IEnumerator RotateToTarget()
    {
        isRotating = true;
        rotatingToTarget = true;
        rotationStartTime = Time.time;
        yield return null;
    }

    private System.Collections.IEnumerator RotateToOriginal()
    {
        isRotating = true;
        rotatingToTarget = false;
        rotationStartTime = Time.time;
        yield return null;
    }
}