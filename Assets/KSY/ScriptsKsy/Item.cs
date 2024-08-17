using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// 0 두유, 1 담배, 2 다이어리, 3 선물, 4 1층 열쇠, 5 2층 열쇠, 6 공구 벨트, 7 니퍼
public class Item : MonoBehaviour
{
    public int itemId;
    public string itemName;
    public Sprite iconImage;
    public GameObject itemModel;
    public string description;
    public bool hasFun;
    public Camera itemCamera;


    private void Start()
    {
        
    }

    public void Use()
    {
        if(!hasFun)
        {
            Debug.Log("이 아이템은 아무런 기능도 없습니다.");
            return;
        }
        Debug.Log("사용된 아이템은 :" + itemId);
        switch(itemId)
        {
            case 0:
                SoyMilk();
                break;
            case 1:
                Soju();
                break;
            default:
                Debug.Log("아이템 ID가 인식되지 않았어요");
                break;
        }
    }

    private void SoyMilk()
    {
        PlayerHealth ph = FindObjectOfType<PlayerHealth>();
        if(ph != null)
        {
            ph.Heal(1);
        }
        else
        {
            Debug.LogError("PlayerHealth가 씬에 없음");
        }
    }

    private void Soju()
    {
        PlayerHealth ph = FindAnyObjectByType<PlayerHealth>();
        if(ph != null)
        {
            ph.TakeDamage(1);
        }
        else
        {
            Debug.LogError("PlayerHealth가 씬에 없음");
        }
    }

}



