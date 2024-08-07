using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public string DriverName; // 드라이버의 이름

    // 열쇠 획득 시 호출되는 메소드
    public void Collect()
    {
        Debug.Log(DriverName + " Paper collected!");
        Destroy(gameObject); // 드라이버를 수집하면 오브젝트를 삭제합니다.
    }
}