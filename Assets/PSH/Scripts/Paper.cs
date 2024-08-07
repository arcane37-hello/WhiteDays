using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public string PaperName; // 종이의 이름

    // 열쇠 획득 시 호출되는 메소드
    public void Collect()
    {
        Debug.Log(PaperName + " Paper collected!");
        Destroy(gameObject); // 종이를 수집하면 오브젝트를 삭제합니다.
    }
}