using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : MonoBehaviour
{
    // PlayerHealth 인스턴스 참조를 위한 변수
    public PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        // 태그가 "Player"인 오브젝트가 콜라이더에 닿았을 때
        if (other.CompareTag("Player"))
        {
            // PlayerHealth 스크립트의 isHiding 변수를 true로 설정
            if (playerHealth != null)
            {
                playerHealth.ishiding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 태그가 "Player"인 오브젝트가 콜라이더를 벗어났을 때
        if (other.CompareTag("Player"))
        {
            // PlayerHealth 스크립트의 isHiding 변수를 false로 설정
            if (playerHealth != null)
            {
                playerHealth.ishiding = false;
            }
        }
    }
}