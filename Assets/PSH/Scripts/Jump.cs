using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight = 10.0f; // ���� ����
    public float gravity = -9.81f; // �߷�

    private float yVelocity = 0.0f; // y�� ���� �ӵ�
    private CharacterController cc;
    public bool canJump;

    void Start()
    {
        canJump = true;
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //// ���� �Է� Ȯ��
        //if (Input.GetButtonDown("Jump") && canJump == true)
        //{
        //    // ���� �ӵ� ���
        //    yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        //// ���߿��� �߷� ����
        //yVelocity += gravity * Time.deltaTime;

        //// ĳ������ �ӵ� ����
        //Vector3 move = new Vector3(0, yVelocity, 0);
        //cc.Move(move * Time.deltaTime);
    }
}