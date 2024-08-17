using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public PlayerHealth ph;
    public SoloText st;


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
                    if (ph.isComplete == false)
                    {
                        st.Hon7();
                    }
                    else if(ph.isComplete == true)
                    {
                        // 달려나가는 애니메이션, 문이 열리는 연출, 점차 페이드아웃 되면서 
                        // 3번 씬으로 전환
                        SceneManager.LoadScene(3);
                    }
                   
                }
            }
        }
    }
}