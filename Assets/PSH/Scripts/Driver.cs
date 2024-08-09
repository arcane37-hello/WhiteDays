using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public string driverName; // 드라이버의 이름

    public void Collect()
    {
        // 플레이어의 인벤토리 상태를 업데이트합니다.
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.SetDriver(true);
        }
        Debug.Log(driverName + " driver collected!");
        Destroy(gameObject); // 드라이버를 수집하면 오브젝트를 삭제합니다.
    }
}