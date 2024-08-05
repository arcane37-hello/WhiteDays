using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float targetZ = 10f; // 이동할 목표 Z 좌표
    public float moveDuration = 1f; // 이동 시간

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y, targetZ);
    }

    public void Move()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // 정확한 위치 보정
        isMoving = false;
    }
}