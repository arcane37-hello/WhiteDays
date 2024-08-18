using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    public GameObject targetObject; // ���̵� ���� ������ ��� ������Ʈ
    public Sit sit;
    public PlayerHealth ph;

    void Start()
    {
        // targetObject�� ��Ȱ��ȭ ���·� ����
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ph.inVent = false;
            sit.ScaledUp();

            if (targetObject != null)
            {
                // targetObject�� Ȱ��ȭ
                targetObject.SetActive(true);

                // targetObject�� Fade ��ũ��Ʈ�� ������ �ִ��� Ȯ��
                Fade fadeScript = targetObject.GetComponent<Fade>();
                if (fadeScript != null)
                {
                    fadeScript.StartFading(); // ���̵� ���� �����ϴ� �޼��� ȣ��
                }
                else
                {
                    Debug.LogError("Target object does not have a Fade script.");
                }

                Destroy(gameObject); // ���� ������Ʈ�� ����
            }
            else
            {
                Debug.LogError("Target object is not assigned.");
            }
        }
    }
}