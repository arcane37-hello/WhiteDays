using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nipper : MonoBehaviour
{
    public string nipperName; // 열쇠의 이름

    public void Collect()
    {
        // 플레이어의 인벤토리 상태를 업데이트합니다.
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.SetNipper(true);
        }
        Debug.Log(nipperName + " nipper collected!");
        Destroy(gameObject); // 니퍼를 수집하면 오브젝트를 삭제합니다.
    }
}