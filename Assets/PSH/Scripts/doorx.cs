using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorx : MonoBehaviour
{
    public float targetX = 10f; // ������ x ��ǥ
    public float moveDuration = 1f; // �̵��� �ð� (��)
    public PlayerInventory playerInventory; // PlayerInventory �ν��Ͻ�

    private Vector3 originalPosition;
    private bool isMoving = false;
    private float moveStartTime;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // �÷��̾ Key�� ������ �ִ��� Ȯ��
        if (playerInventory != null && playerInventory.hasKey)
        {
            // ���콺 ���� Ŭ�� ����
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Ŭ���� ������Ʈ�� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ���� Ȯ��
                    if (hit.transform == transform && !isMoving)
                    {
                        StartCoroutine(MoveToTargetX());
                    }
                }
            }

            // �̵� �߿��� �ε巴�� �����̵��� ó��
            if (isMoving)
            {
                float elapsedTime = Time.time - moveStartTime;
                float t = Mathf.Clamp01(elapsedTime / moveDuration);
                Vector3 newPosition = Vector3.Lerp(originalPosition, new Vector3(targetX, originalPosition.y, originalPosition.z), t);
                transform.position = newPosition;

                // �̵� �Ϸ� üũ
                if (t >= 1f)
                {
                    isMoving = false;
                    // ���� ��ġ�� ���ư����� �Ϸ��� ���� �ּ��� �����ϼ���.
                    // StartCoroutine(MoveBackToOriginalPosition());
                }
            }
        }
    }

    private System.Collections.IEnumerator MoveToTargetX()
    {
        isMoving = true;
        moveStartTime = Time.time;
        yield return null;
    }

    private System.Collections.IEnumerator MoveBackToOriginalPosition()
    {
        float moveBackDuration = moveDuration; // ���� ��ġ�� ���ư��� �ð� (���� �ð� ���� �̵�)
        Vector3 startPosition = transform.position;
        float moveBackStartTime = Time.time;

        while (Time.time - moveBackStartTime < moveBackDuration)
        {
            float t = (Time.time - moveBackStartTime) / moveBackDuration;
            transform.position = Vector3.Lerp(startPosition, originalPosition, t);
            yield return null;
        }
        transform.position = originalPosition; // ������ ��ġ�� ��Ȯ�� ����
    }
}