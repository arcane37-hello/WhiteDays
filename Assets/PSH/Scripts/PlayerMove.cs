using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    public float sprintSpeed = 14.0f;  // ���� �� �ӵ�
    public float walkSpeed = 3.5f;  // �ȱ� �ӵ�
    public float rotSpeed = 200.0f;
    public float yVelocity = 2.0f;
    public float stamina = 100.0f;  // ���¹̳� ������ public���� ����

    private bool isCoolingDown = false;
    private float originalMoveSpeed;
    private bool isSprinting = false;  // ���� ���� ���θ� ��Ÿ���� ����

    float rotX;
    float rotY;
    float yPos;

    CharacterController cc;
    Vector3 gravityPower;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;
    
        cc = GetComponent<CharacterController>();
        gravityPower = Physics.gravity;
        yPos = transform.position.y;

        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if(stamina > 0)
        {
            Move();
        }
        else
        {
            if (!isCoolingDown)
            {
                StartCoroutine(CooldownCoroutine());
            }
        }
        
        Rotate();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // ���� �Է� Ȯ��
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0)
        {
            isSprinting = false;
        }

        // ���� ���¿� ���� �ӵ� ����
        if (isSprinting)
        {
            moveSpeed = sprintSpeed;
            stamina -= 5.0f * Time.deltaTime;  // ���� �� ���¹̳� �Ҹ�
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = walkSpeed;
            stamina += 3.0f * Time.deltaTime;  // �ȱ� �� ���¹̳� ȸ��
        }
        else
        {
            moveSpeed = originalMoveSpeed;  // �⺻ �̵� �ӵ��� ����
            stamina += 3.0f * Time.deltaTime;  // ���¹̳� ȸ��
        }

        stamina = Mathf.Clamp(stamina, 0.0f, 100.0f);  // ���¹̳��� 0���� 100 ���̷� ����

        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        dir.Normalize();

        yPos += gravityPower.y * yVelocity * Time.deltaTime;
        if (cc.isGrounded)
        {
            yPos = 0;
        }
        dir.y = yPos;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotX += mouseY * rotSpeed * Time.deltaTime;
        rotY += mouseX * rotSpeed * Time.deltaTime;

        // ���� ȸ�� ���� ����
        if (rotX > 80.0f)
        {
            rotX = 80.0f;
        }
        else if (rotX < -80.0f)
        {
            rotX = -80.0f;
        }

        transform.eulerAngles = new Vector3(-rotX, rotY, 0);
        Camera.main.transform.GetComponent<FollowCamera>().rotX = rotX;
    }

    IEnumerator CooldownCoroutine()
    {
        isCoolingDown = true;
        isSprinting = false;

        // 3�� ���� ����մϴ�.
        yield return new WaitForSeconds(7f);

        // 3�� ��, ���¹̳��� �������ϰų� �ٸ� �۾��� �����մϴ�.
        isCoolingDown = false;
        stamina = 100.0f;
        
    }
}












// ���¹̳� ���� �� �ڵ�
//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 7.0f;
//    public float rotSpeed = 200.0f;
//    public float yVelocity = 2.0f;
//    public float jumpPower = 4.0f;

//    private float originalMoveSpeed;

//    // ȸ�� ���� �̸� ����ϱ� ���� ȸ����(x, y) ����
//    float rotX;
//    float rotY;
//    float yPos;

//    CharacterController cc;

//    // �߷��� �����ϰ� �ʹ�.
//    // �ٴڿ� �浹�� ���� �������� �Ʒ��� ��� �������� �ϰ� �ʹ�.
//    // ����: �Ʒ�, ũ��: �߷�

//    Vector3 gravityPower;

//    void Start()
//    {
//        // ������ ȸ�� ���´�� ������ �ϰ� �ʹ�.
//        rotX = transform.eulerAngles.x;
//        rotY = transform.eulerAngles.y;

//        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� ������ ��Ƴ��´�.
//        cc = GetComponent<CharacterController>();

//        // �߷� ���� �ʱ�ȭ�Ѵ�.
//        gravityPower = Physics.gravity;
//        yPos = transform.position.y;

//        originalMoveSpeed = moveSpeed;

//    }

//    // "Horizontal"�� "Vertical" �Է��� �̿��ؼ� ��������� �̵��ϰ� �ϰ� �ʹ�.
//    // 1. ������� �Է��� �޴´�.
//    // 2. ����, �ӷ��� ����Ѵ�.
//    // 3. �� �����Ӹ��� ���� �ӵ��� �ڽ��� ��ġ�� �����Ѵ�.
//    void Update()
//    {
//        Move();
//        Rotate();



//    }

//    void Move()
//    {
//        float h = Input.GetAxis("Horizontal");
//        float v = Input.GetAxis("Vertical");


//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            moveSpeed = 14.0f;
//        }

//        else if (Input.GetKey(KeyCode.LeftControl))
//        {
//            moveSpeed = 3.5f;
//        }

//        else
//        {
//            moveSpeed = originalMoveSpeed;  // Shift Ű�� ���� ������ �ӵ��� �����Ѵ�.
//        }



//        // ���� ���� ���⿡ ���� �̵��ϵ��� �����Ѵ�.
//        // 1. ���� ���� ���͸� �̿��ؼ� ����ϴ� ���
//        // Vector3 dir = transform.forward * v + transform.right * h;
//        // dir.Normalize();

//        // 2. ���� ȸ�� ���� ���� ���� ���� ���͸� ���� ������ ���ͷ� ��ȯ�ϴ� �Լ��� �̿��ϴ� ���
//        Vector3 dir = new Vector3(h, 0, v); // ���� ���� ����
//        dir = transform.TransformDirection(dir);
//        dir.Normalize();

//        // 2. ���� �̵� ���

//        // �߷� ����
//        yPos += gravityPower.y * yVelocity * Time.deltaTime;

//        // �ٴڿ� ������� ������ yPos�� ���� 0���� �ʱ�ȭ�Ѵ�.
//        if (cc.collisionFlags == CollisionFlags.CollidedBelow)
//        {
//            yPos = 0;
//        }


//        dir.y = yPos;

//        // transform.position += dir * moveSpeed * Time.deltaTime;
//        cc.Move(dir * moveSpeed * Time.deltaTime);
//        // cc.SimpleMove(dir * moveSpeed * Time.deltaTime);


//    }

//    // ������� ���콺 �巡�� ���⿡ ���� ���� �����¿� ȸ���� �ǰ� �ϰ� �ʹ�.
//    // 1. ������� ���콺 �巡�� �Է��� �޴´�.
//    // 2. ȸ�� �ӷ�, ȸ�� ������ �ʿ��ϴ�.
//    // 3. �� �����Ӹ��� ���� �ӵ��� �ڽ��� ȸ������ �����Ѵ�.
//    void Rotate()
//    {
//        float mouseX = Input.GetAxis("Mouse X");
//        float mouseY = Input.GetAxis("Mouse Y");

//        // �� �� ���� ȸ�� ���� �̸� ����Ѵ�.
//        rotX += mouseY * rotSpeed * Time.deltaTime;
//        rotY += mouseX * rotSpeed * Time.deltaTime;

//        // ���� ȸ���� -80�� ~ +80�������� �����Ѵ�.


//        if (rotX > 80.0f)
//        {
//            rotX = 80.0f;
//        }
//        else if (rotX < -80.0f)
//        {
//            rotX = -80.0f;
//        }

//        // ���� ȸ�� ���� ���� Ʈ������ ȸ�� ������ �����Ѵ�.
//        transform.eulerAngles = new Vector3(-rotX, rotY, 0);
//        Camera.main.transform.GetComponent<FollowCamera>().rotX = rotX;
//    }
//}
