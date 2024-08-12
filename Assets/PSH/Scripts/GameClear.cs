using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    private void Update()
    {
        // 왼쪽 마우스 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인
                if (hit.transform == transform)
                {
                    // 3번 씬으로 전환
                    SceneManager.LoadScene(3);
                }
            }
        }
    }
}