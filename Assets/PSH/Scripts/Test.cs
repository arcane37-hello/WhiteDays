using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject objectToMove;  // 이동할 오브젝트
    public Transform newPosition;    // 이동할 위치
    public PlayerInventory playerInventory; // PlayerInventory 인스턴스
    public Sit sit;
    public AudioClip nipper;
    public PlayerHealth ph;

    private void Update()
    {
        // 플레이어가 Nipper를 가지고 있는지 확인
        if (playerInventory != null && playerInventory.hasNipper)
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
                    if (hit.transform.gameObject == gameObject)
                    {
                        // objectToMove를 newPosition으로 이동
                        if (objectToMove != null && newPosition != null)
                        {
                            ph.inVent = true;
                            sit.ScaledDown();
                            objectToMove.transform.position = newPosition.position;
                        }

                        // 현재 오브젝트를 파괴
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}