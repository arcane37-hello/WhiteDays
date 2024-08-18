using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper1 : MonoBehaviour
{
    public string paperName; // 종이의 이름

    public void Collect()
    {
        // 플레이어의 인벤토리 상태를 업데이트합니다.
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.SetPaper(true);
        }
        Debug.Log(paperName + " paper collected!");
        // Destroy(gameObject); // 종이를 수집하면 오브젝트를 삭제합니다.
    }
}