using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭이 감지되었는지 확인
        if (Input.GetMouseButtonDown(0)) // 0은 왼쪽 마우스 버튼
        {
            // 클릭된 위치를 화면에서 월드 좌표로 변환
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast를 사용하여 클릭된 오브젝트 감지
            if (Physics.Raycast(ray, out hit))
            {
                // 클릭된 오브젝트가 현재 스크립트가 붙어 있는 오브젝트인지 확인
                if (hit.transform == transform)
                {
                    // 오브젝트를 삭제
                    Destroy(gameObject);
                }
            }
        }
    }
}
