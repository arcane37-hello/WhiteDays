using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public Canvas lockUI; // 자물쇠 UI 오브젝트

    private void Start()
    {
        lockUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
        {
            // 마우스 클릭 위치의 레이캐스트를 통해 자물쇠 오브젝트가 클릭되었는지 확인
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // 자물쇠 오브젝트를 클릭했을 경우
                {
                    if (lockUI != null)
                    {
                        lockUI.gameObject.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;


                    }
                }
            }
        }
    }
}
