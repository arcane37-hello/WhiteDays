using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight = 10.0f; // 점프 높이
    public float gravity = -9.81f; // 중력

    private float yVelocity = 0.0f; // y축 방향 속도
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 점프 입력 확인
        if (Input.GetButtonDown("Jump"))
        {
            // 점프 속도 계산
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 공중에서 중력 적용
        yVelocity += gravity * Time.deltaTime;

        // 캐릭터의 속도 설정
        Vector3 move = new Vector3(0, yVelocity, 0);
        cc.Move(move * Time.deltaTime);
    }
}