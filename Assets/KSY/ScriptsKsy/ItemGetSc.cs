using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetSc : MonoBehaviour
{
    public float pickRange = 1.5f;

    public MMSc mmsc;

    public Camera pCam;
    public Image defaultAim;
    public Image selectAim;

    public GameObject diary;
    public GameObject present;
    public GameObject firstPaper;

    public PlayerHealth ph;

    public SoloText sT;
    public Complete cm;

    void Start()
    {
        selectAim.enabled = false;
        defaultAim.enabled = true;

        AddInitialItems();
    }

    void Update()
    {
        UpdateAim();
        if (Input.GetMouseButtonDown(0))
        {
            TryPickupItem();
        }
    }

    void UpdateAim()
    {
        Ray ray = new Ray(pCam.transform.position, pCam.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, pickRange))
        {
            if(hit.collider.GetComponent<Item>() != null || hit.collider.GetComponent<Paper>() !=null || hit.collider.GetComponent<Complete>() != null)
            {
                defaultAim.enabled = false;
                selectAim.enabled =true;
            }
            else
            {
                defaultAim.enabled = true;
                selectAim.enabled = false;
            }
        }
        else
        {
            defaultAim.enabled = true;
            selectAim.enabled = false;
        }
    }

    void TryPickupItem()
    {
        Ray ray = new Ray(pCam.transform.position, pCam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickRange))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null && item.itemId != 6 && item.itemId != 7)
            {
                mmsc.AddItemToInventory(item);
                Destroy(item.gameObject);
                mmsc.GetItem();
                sT.Hon4();
                Debug.Log("������ ����:" + item.itemId);
                return;
            }
            else if(item != null && item.itemId == 6)
            {
                if(ph.canDriver == true )
                {
                    mmsc.AddItemToInventory(item);
                    Destroy(item.gameObject);
                    mmsc.GetItem();
                    sT.Hon4();
                    Debug.Log("������ ����:" + item.itemId);
                    return;
                }
                else if(ph.canDriver == false )
                {
                    sT.Hon10();
                    return;
                }
            }
            else if (item != null && item.itemId == 7)
            {
                // �÷��̾��� �κ��丮 ���¸� ������Ʈ�մϴ�.
                PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
                if (playerInventory != null)
                {
                    playerInventory.SetNipper(true);
                }
                mmsc.AddItemToInventory(item);
                Destroy(item.gameObject);
                mmsc.GetItem();
                sT.Hon4();
                Debug.Log("������ ����:" + item.itemId);
                return;
            }

            Paper paper = hit.collider.GetComponent<Paper>();
            if (paper != null)
            {
                mmsc.AddPaperToInventory(paper);
                Destroy(paper.gameObject);
                mmsc.GetPaper();
                sT.Hon5();
                Debug.Log("���� ����:" + paper.paperId);
                if (paper.isDriver == true)
                {
                    ph.canDriver = true;
                }
                return;
            }
            Debug.Log("������ �����۵� �ƴ�");

            Complete cm = hit.collider.GetComponent<Complete>();
            if (cm != null && cm.isLast == false)
            {
                sT.Hon9();
                return;
            }
            else if(cm != null && cm.isLast == true)
            {
                Item item1 = mmsc.itemOrder[0];
                mmsc.RemoveItem(item1);
                Item item2 = mmsc.itemOrder[0];
                mmsc.RemoveItem(item2);
                sT.Hon8();
                ph.isComplete = true;
                return;
            }
        }
        else
        {
            Debug.Log("���� ���� �ƹ� ������Ʈ�� ����");
        }
    }

    void AddInitialItems()
    {
        if(diary !=null)
        {
            GameObject diaryItem = diary;
            Item item = diaryItem.GetComponent<Item>();
            if (item != null)
            {
                mmsc.AddItemToInventory(item);
                Debug.Log("���̾�� �κ��丮�� �߰��Ǿ����ϴ�.");
            }
        }
        if (present != null)
        {
            GameObject presentItem = present;
            Item item = presentItem.GetComponent<Item>();
            if(item != null)
            {
                mmsc.AddItemToInventory(item);
                Debug.Log("������ �κ��丮�� �߰��Ǿ����ϴ�.");
            }
        }
        if (firstPaper != null)
        {
            GameObject fP = firstPaper;
            Paper paper = fP.GetComponent<Paper>();
            if (paper != null)
            {
                mmsc.AddPaperToInventory(paper);
                Debug.Log("�⺻ ������ �κ��丮�� �߰��Ǿ����ϴ�.");
            }
        }
    }
}
