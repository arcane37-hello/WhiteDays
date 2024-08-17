using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    public float targetZ = 10f; // 지정할 z 좌표
    public float moveDuration = 1f; // 이동할 시간 (초)
    public PlayerInventory playerInventory; // PlayerInventory 인스턴스

    private Vector3 originalPosition;
    private bool isMoving = false;
    private float moveStartTime;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // 플레이어가 Key를 가지고 있는지 확인
        if (playerInventory != null && playerInventory.hasKey2)
        {
            // 마우스 왼쪽 클릭 감지
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // 클릭한 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인
                    if (hit.transform == transform && !isMoving)
                    {
                        StartCoroutine(MoveToTargetZ());
                    }
                }
            }

            // 이동 중에는 부드럽게 움직이도록 처리
            if (isMoving)
            {
                float elapsedTime = Time.time - moveStartTime;
                float t = Mathf.Clamp01(elapsedTime / moveDuration);
                Vector3 newPosition = Vector3.Lerp(originalPosition, new Vector3(originalPosition.x, originalPosition.y, targetZ), t);
                transform.position = newPosition;

                // 이동 완료 체크
                if (t >= 1f)
                {
                    isMoving = false;
                    // 원래 위치로 돌아가도록 하려면 다음 주석을 해제하세요.
                    // StartCoroutine(MoveBackToOriginalPosition());
                }
            }
        }
    }

    private IEnumerator MoveToTargetZ()
    {
        isMoving = true;
        moveStartTime = Time.time;
        yield return null;
    }

    private IEnumerator MoveBackToOriginalPosition()
    {
        float moveBackDuration = moveDuration; // 원래 위치로 돌아가는 시간 (같은 시간 동안 이동)
        Vector3 startPosition = transform.position;
        float moveBackStartTime = Time.time;

        while (Time.time - moveBackStartTime < moveBackDuration)
        {
            float t = (Time.time - moveBackStartTime) / moveBackDuration;
            transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            yield return null;
        }
        transform.position = originalPosition; // 마지막 위치를 정확히 설정
    }
}