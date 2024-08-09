using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{
    // 초기 스케일 값
    private Vector3 initialScale = new Vector3(0.4f, 0.6f, 0.4f);

    // 변경할 스케일 값
    private Vector3 scaledDown = new Vector3(0.2f, 0.3f, 0.2f);

    // 현재 스케일 상태
    private Vector3 targetScale;

    private void Start()
    {
        // 초기 스케일 설정
        transform.localScale = initialScale;
        targetScale = initialScale;
    }

    private void Update()
    {
        // 왼쪽 컨트롤 키가 눌렸는지 체크
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // 왼쪽 컨트롤 키가 눌리면 스케일을 변경
            targetScale = scaledDown;
        }
        else
        {
            // 키가 눌리지 않으면 원래 스케일로 돌아감
            targetScale = initialScale;
        }

        // 스케일을 변경
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5f);
    }
}