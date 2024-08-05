using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDalsu : MonoBehaviour
{
    public GameObject objectToActivate; // 활성화할 오브젝트

    private void Start()
    {
        // 오브젝트를 비활성화 상태로 시작합니다.
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인합니다.
        if (other.CompareTag("Player"))
        {
            // objectToActivate가 비어있지 않으면 활성화합니다.
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log("Activated the object: " + objectToActivate.name);
            }
            else
            {
                Debug.LogWarning("Object to activate is not assigned.");
            }
        }
    }
}