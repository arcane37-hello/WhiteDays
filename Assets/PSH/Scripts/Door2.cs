using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public float rotationAngle = 90f; // ȸ���� ���� (�� ����)
    public float rotationDuration = 1f; // ȸ���� �ð� (��)

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private bool rotatingToTarget = false;
    private float rotationStartTime;

    private PlayerInventory playerInventory; // �÷��̾��� �κ��丮 ���¸� ������ ����

    public SoloText sT;
    public AudioClip open;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation * Quaternion.Euler(0, rotationAngle, 0);

        // �÷��̾��� PlayerInventory ������Ʈ�� ã�Ƽ� �Ҵ��մϴ�.
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        // ���콺 ���� Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ������Ʈ�� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ���� Ȯ��
                if (hit.transform == transform)
                {
                    // �÷��̾ ���踦 ������ �ִ��� Ȯ��
                    if (playerInventory != null && playerInventory.hasKey)
                    {
                        if (!isRotating)
                        {
                            if (rotatingToTarget)
                            {
                                // ���� ȸ�� ���·� ���ư�����
                                StopAllCoroutines();
                                StartCoroutine(RotateToOriginal());
                            }
                            else
                            {
                                AudioSource.PlayClipAtPoint(open, transform.position);
                                // ��ǥ ȸ������ ȸ���ϵ���
                                StartCoroutine(RotateToTarget());
                            }
                        }
                    }
                    else
                    {
                        sT.Hon6();
                        Debug.Log("You need a key to open this door.");
                    }
                }
            }
        }

        // ȸ�� �߿��� �ε巴�� ȸ���ϵ��� ó��
        if (isRotating)
        {
            float elapsedTime = Time.time - rotationStartTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            Quaternion startRotation = rotatingToTarget ? originalRotation : targetRotation;
            Quaternion endRotation = rotatingToTarget ? targetRotation : originalRotation;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ȸ�� �Ϸ� üũ
            if (t >= 1f)
            {
                isRotating = false;
                rotatingToTarget = !rotatingToTarget; // ȸ�� ���� ��ȯ
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