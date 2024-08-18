using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockManager : MonoBehaviour
{
    public static LockManager lm;

    public Text feedbackText; // UI �ؽ�Ʈ
    public Button[] numberButtons; // ���� ��ư �迭
    public GameObject lockObject; // �ڹ��� ������Ʈ
    public LockDoor lockDoorScript; // �ڹ��谡 �ɷ� �ִ� ���� ���� LockDoor ��ũ��Ʈ ����
    public Canvas locker;

    public string password = "1234"; // ������ ��й�ȣ
    public string inputPassword = ""; // �Էµ� ��й�ȣ

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
        // ��ư Ŭ�� �̺�Ʈ ���
        foreach (Button button in numberButtons)
        {
            button.onClick.AddListener(() => OnNumberButtonClicked(button));
        }
    }

    public void OnNumberButtonClicked(Button button)
    {
        // ��ư�� ���� ���� ��������
        string number = button.GetComponentInChildren<Text>().text;
        inputPassword += number;
        Debug.Log("��ư�� ����");

        // ��й�ȣ ���� Ȯ��
        if (inputPassword.Length == password.Length)
        {
            CheckPassword();
        }
    }

    private void CheckPassword()
    {
        if (inputPassword == password)
        {
            // ��й�ȣ ����
            Destroy(lockObject); // �ڹ��� ������Ʈ �ı�
            if (lockDoorScript != null)
            {
                lockDoorScript.UnlockDoor(); // �� ��� ����
            }
            feedbackText.text = "Unlocked!";
            Destroy(locker);
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            // ��й�ȣ Ʋ��
            feedbackText.text = "Error";
            inputPassword = ""; // ��й�ȣ �ʱ�ȭ
        }
    }
}
