using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public float targetRotationX = -90f;  // 목표 x 회전값
    public float duration = 1f;           // 회전 애니메이션 시간

    private bool isStart = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStart) // 마우스 왼쪽 버튼 클릭 확인
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 오브젝트가 현재 스크립트가 붙어있는 오브젝트인지 확인
                if (hit.transform == transform)
                {
                    StartCoroutine(RotRoutine(duration, targetRotationX));
                }
            }
        }
    }

    IEnumerator RotRoutine(float time, float degree)
    {
        isStart = true;

        int count = 0;
        int wholeCount = (int)(time / 0.02f);

        while (count < wholeCount)
        {
            count++;
            transform.Rotate(new Vector3(0, degree * 0.02f, 0));

            yield return null;
        }

        isStart = false;
    }
}