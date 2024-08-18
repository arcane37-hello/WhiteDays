using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDalsu2 : MonoBehaviour
{
    public GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ
    public PlayerHealth ph;

    private void Start()
    {
        // ������Ʈ�� ��Ȱ��ȭ ���·� �����մϴ�.
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ���մϴ�.
        if (other.CompareTag("Player"))
        {
            if(ph.isComplete == true)
            {
                // objectToActivate�� ������� ������ Ȱ��ȭ�մϴ�.
                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);
                    Debug.Log("Activated the object: " + objectToActivate.name);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Object to activate is not assigned.");
                }
            }          
        }
    }
}