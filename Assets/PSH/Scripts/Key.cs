using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName; // 열쇠의 이름

    // 열쇠 획득 시 호출되는 메소드
    public void Collect()
    {
        Debug.Log(keyName + " key collected!");
        Destroy(gameObject); // 열쇠를 수집하면 오브젝트를 삭제합니다.
    }
}