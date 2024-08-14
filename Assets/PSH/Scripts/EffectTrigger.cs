using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    public GameObject targetObject; // 페이드 인을 적용할 대상 오브젝트

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // targetObject가 Fade 스크립트를 가지고 있는지 확인
            Fade fadeScript = targetObject.GetComponent<Fade>();
            if (fadeScript != null)
            {
                fadeScript.StartFading(); // 페이드 인을 시작하는 메서드 호출
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Target object does not have a Fade script.");
            }
        }
    }
}