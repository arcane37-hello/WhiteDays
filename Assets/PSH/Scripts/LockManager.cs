using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockManager : MonoBehaviour
{
    public static LockManager lm;

    public Text feedbackText; // UI 텍스트
    public Button[] numberButtons; // 숫자 버튼 배열
    public GameObject lockObject; // 자물쇠 오브젝트
    public LockDoor lockDoorScript; // 자물쇠가 걸려 있는 문에 대한 LockDoor 스크립트 참조
    public Canvas locker;

    public string password = "1234"; // 설정된 비밀번호
    public string inputPassword = ""; // 입력된 비밀번호

    void Awake()
    {
        if(lm == null)
        {
            lm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        foreach (Button button in numberButtons)
        {
            button.onClick.AddListener(() => OnNumberButtonClicked(button));
        }
    }

    public void OnNumberButtonClicked(Button button)
    {
        // 버튼에 적힌 숫자 가져오기
        string number = button.GetComponentInChildren<Text>().text;
        inputPassword += number;
        Debug.Log("버튼이 눌림");

        // 비밀번호 길이 확인
        if (inputPassword.Length == password.Length)
        {
            CheckPassword();
        }
    }

    private void CheckPassword()
    {
        if (inputPassword == password)
        {
            // 비밀번호 맞춤
            Destroy(lockObject); // 자물쇠 오브젝트 파괴
            if (lockDoorScript != null)
            {
                lockDoorScript.UnlockDoor(); // 문 잠금 해제
            }
            feedbackText.text = "Unlocked!";
            Destroy(locker);
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            // 비밀번호 틀림
            feedbackText.text = "Error";
            inputPassword = ""; // 비밀번호 초기화
        }
    }
}
