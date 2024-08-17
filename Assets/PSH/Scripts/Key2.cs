using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    public string keyName; // 열쇠의 이름

    public void Collect()
    {
        // 플레이어의 인벤토리 상태를 업데이트합니다.
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.SetKey2(true);
        }
        Debug.Log(keyName + " key collected!");
        Destroy(gameObject); // 열쇠를 수집하면 오브젝트를 삭제합니다.
    }
}
