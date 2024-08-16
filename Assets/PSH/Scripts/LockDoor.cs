using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    private bool isLocked = true; // 문이 잠겨있는 상태

    // 문을 잠금 해제하는 메서드
    public void UnlockDoor()
    {
        isLocked = false;
        // 문을 열 수 있는 로직 추가 (예: 스프라이트 변경, 애니메이션 재생 등)
        Debug.Log("Door is now unlocked.");
        // 예를 들어, 문을 열기 위해 활성화 상태 변경 또는 애니메이션 트리거를 추가할 수 있습니다.
    }

    private void Update()
    {
        if(isLocked == false)
        {
            Destroy(gameObject);
        }
    }
}