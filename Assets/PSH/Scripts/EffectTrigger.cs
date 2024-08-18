using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    public GameObject targetObject; // 페이드 인을 적용할 대상 오브젝트
    public Sit sit;
    public PlayerHealth ph;

    void Start()
    {
        // targetObject를 비활성화 상태로 설정
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
                // targetObject를 활성화
                targetObject.SetActive(true);

                // targetObject가 Fade 스크립트를 가지고 있는지 확인
                Fade fadeScript = targetObject.GetComponent<Fade>();
                if (fadeScript != null)
                {
                    fadeScript.StartFading(); // 페이드 인을 시작하는 메서드 호출
                }
                else
                {
                    Debug.LogError("Target object does not have a Fade script.");
                }

                Destroy(gameObject); // 현재 오브젝트를 삭제
            }
            else
            {
                Debug.LogError("Target object is not assigned.");
            }
        }
    }
}