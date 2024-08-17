using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    public float sprintSpeed = 14.0f;
    public float walkSpeed = 3.5f;
    public float rotSpeed = 200.0f;
    public float yVelocity = 2.0f;
    public float stamina = 100.0f;
    public bool canMove;
    public bool canRot;

    public AudioClip walk;
    public AudioClip run;
    public AudioClip crawl;

    private bool isCoolingDown = false;
    private float originalMoveSpeed;
    private bool isSprinting = false;
    private float standing = 0f;

    float rotX;
    float rotY;
    float yPos;

    CharacterController cc;
    Vector3 gravityPower;

    Animator animator; // 애니메이터 컴포넌트
    AudioSource audioSource; // AudioSource 컴포넌트

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;

        cc = GetComponent<CharacterController>();
        gravityPower = Physics.gravity;
        yPos = transform.position.y;

        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

        originalMoveSpeed = moveSpeed;
        canMove = true;
        canRot = true;

        // AudioSource 컴포넌트 추가
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // 3D 사운드 설정
        audioSource.volume = 1.0f; // 기본 음량 설정
    }

    void Update()
    {
        if (stamina > 0 && canMove)
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

        if (canRot)
        {
            Rotate();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float threshold = 0.1f; // 아주 작은 값, 이 값 이하일 때 입력이 없다고 판단

        // 입력이 없거나 거의 없을 때
        if (Mathf.Abs(h) < threshold && Mathf.Abs(v) < threshold)
        {
            animator.Play("Stand");
        }
        else
        {
            MoveState();
        }

        // 질주 입력 확인
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0)
        {
            isSprinting = false;
        }

        // 스태미나를 0에서 100 사이로 제한
        stamina = Mathf.Clamp(stamina, 0.0f, 100.0f);

        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        dir.Normalize();

        // 중력 적용 및 위치 업데이트
        yPos += gravityPower.y * yVelocity * Time.deltaTime;
        if (cc.isGrounded)
        {
            yPos = 0;
        }
        dir.y = yPos;

        // 캐릭터 이동
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotX += mouseY * rotSpeed * Time.deltaTime;
        rotY += mouseX * rotSpeed * Time.deltaTime;

        // 상하 회전 각도 제한
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

        // 7초 동안 대기
        yield return new WaitForSeconds(7f);

        // 7초 후, 스태미나를 재충전
        isCoolingDown = false;
        stamina = 100.0f;
    }

    private void MoveState()
    {
        // 질주 상태에 따라 속도 조정 및 애니메이션 트리거 설정
        if (isSprinting)
        {
            moveSpeed = sprintSpeed;
            stamina -= 5.0f * Time.deltaTime;
            animator.Play("Run");
            PlaySound(run); // 질주 소리 재생
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = walkSpeed;
            stamina += 3.0f * Time.deltaTime;
            animator.Play("Crawl");
            PlaySound(crawl); // 기어가기 소리 재생
        }
        else
        {
            moveSpeed = originalMoveSpeed;
            stamina += 3.0f * Time.deltaTime;
            animator.Play("Walk");
            PlaySound(walk); // 걷기 소리 재생
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            // 현재 재생 중인 소리가 없을 때만 소리 재생
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }
        }
        else
        {
            Debug.LogWarning("Audio clip not assigned!");
        }
    }
}









// 스태미나 구현 전 코드
//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 7.0f;
//    public float rotSpeed = 200.0f;
//    public float yVelocity = 2.0f;
//    public float jumpPower = 4.0f;

//    private float originalMoveSpeed;

//    // 회전 값을 미리 계산하기 위한 회전축(x, y) 변수
//    float rotX;
//    float rotY;
//    float yPos;

//    CharacterController cc;

//    // 중력을 적용하고 싶다.
//    // 바닥에 충돌이 있을 때까지는 아래로 계속 내려가게 하고 싶다.
//    // 방향: 아래, 크기: 중력

//    Vector3 gravityPower;

//    void Start()
//    {
//        // 최초의 회전 상태대로 시작을 하고 싶다.
//        rotX = transform.eulerAngles.x;
//        rotY = transform.eulerAngles.y;

//        // 캐릭터 컨트롤러 컴포넌트를 변수에 담아놓는다.
//        cc = GetComponent<CharacterController>();

//        // 중력 값을 초기화한다.
//        gravityPower = Physics.gravity;
//        yPos = transform.position.y;

//        originalMoveSpeed = moveSpeed;

//    }

//    // "Horizontal"과 "Vertical" 입력을 이용해서 수평면으로 이동하게 하고 싶다.
//    // 1. 사용자의 입력을 받는다.
//    // 2. 방향, 속력을 계산한다.
//    // 3. 매 프레임마다 계산된 속도로 자신의 위치를 변경한다.
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
//            moveSpeed = originalMoveSpeed;  // Shift 키를 떼면 원래의 속도로 복원한다.
//        }



//        // 나의 정면 방향에 따라서 이동하도록 변경한다.
//        // 1. 로컬 방향 벡터를 이용해서 계산하는 방법
//        // Vector3 dir = transform.forward * v + transform.right * h;
//        // dir.Normalize();

//        // 2. 나의 회전 값에 따라서 월드 방향 벡터를 로컬 방향의 벡터로 변환하는 함수를 이용하는 방법
//        Vector3 dir = new Vector3(h, 0, v); // 월드 방향 벡터
//        dir = transform.TransformDirection(dir);
//        dir.Normalize();

//        // 2. 수직 이동 계산

//        // 중력 적용
//        yPos += gravityPower.y * yVelocity * Time.deltaTime;

//        // 바닥에 닿아있을 때에는 yPos의 값을 0으로 초기화한다.
//        if (cc.collisionFlags == CollisionFlags.CollidedBelow)
//        {
//            yPos = 0;
//        }


//        dir.y = yPos;

//        // transform.position += dir * moveSpeed * Time.deltaTime;
//        cc.Move(dir * moveSpeed * Time.deltaTime);
//        // cc.SimpleMove(dir * moveSpeed * Time.deltaTime);


//    }

//    // 사용자의 마우스 드래그 방향에 따라서 나의 상하좌우 회전이 되게 하고 싶다.
//    // 1. 사용자의 마우스 드래그 입력을 받는다.
//    // 2. 회전 속력, 회전 방향이 필요하다.
//    // 3. 매 프레임마다 계산된 속도로 자신의 회전값을 변경한다.
//    void Rotate()
//    {
//        float mouseX = Input.GetAxis("Mouse X");
//        float mouseY = Input.GetAxis("Mouse Y");

//        // 각 축 별로 회전 값을 미리 계산한다.
//        rotX += mouseY * rotSpeed * Time.deltaTime;
//        rotY += mouseX * rotSpeed * Time.deltaTime;

//        // 상하 회전은 -80도 ~ +80도까지로 제한한다.


//        if (rotX > 80.0f)
//        {
//            rotX = 80.0f;
//        }
//        else if (rotX < -80.0f)
//        {
//            rotX = -80.0f;
//        }

//        // 계산된 회전 값을 나의 트랜스폼 회전 값으로 적용한다.
//        transform.eulerAngles = new Vector3(-rotX, rotY, 0);
//        Camera.main.transform.GetComponent<FollowCamera>().rotX = rotX;
//    }
//}
