using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper1 : MonoBehaviour
{
    public string paperName; // ������ �̸�

    public void Collect()
    {
        // �÷��̾��� �κ��丮 ���¸� ������Ʈ�մϴ�.
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.SetPaper(true);
        }
        Debug.Log(paperName + " paper collected!");
        // Destroy(gameObject); // ���̸� �����ϸ� ������Ʈ�� �����մϴ�.
    }
}