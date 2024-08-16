using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// using static UnityEditor.Progress;

public class MMSc : MonoBehaviour
{
    public Canvas escC;
    public SMSSc smss;
    public GameObject icamPrefab;
    private Camera icam;
    public RawImage itemDisplay;
    public RawImage paperDisplay;
    private RenderTexture itemT;
    private RenderTexture paperT;

    private List<Item> itemInventory = new List<Item>();
    private List<Paper> paperInventory = new List<Paper>();

    public GameObject player;

    public AudioClip mnOpen;
    public AudioClip mnClose;
    public AudioClip nope;

    private bool hasMapped;

    public Canvas Ui1;
    public Canvas Ui2;
    public Canvas Ui3;
    public Canvas Ui4;

    public Image itemSelectBG;
    public Image paperSelectBG;
    public GameObject itemNoneSelectPrefab;
    public GameObject paperNoneSelectPrefab;

    public Transform itemIconParent;
    public Transform paperIconParent;

    private GameObject itemPrefab;
    private GameObject paperPrefab;

    public TextMeshProUGUI currentItemTitle;
    public TextMeshProUGUI currentItemInfo;
    public TextMeshProUGUI currentPaperTitle;

    private GameObject currentItem;
    private GameObject currentPaper;

    public Transform itemBg;
    public Transform paperBg;

    public Image itemIcon;
    public TextMeshProUGUI paperName;

    public Canvas currentInventory;

    private List<Item> itemOrder = new List<Item>();
    private Dictionary<Item, Image> itemIcons = new Dictionary<Item, Image>();
    private Dictionary<Item, GameObject> itemModels = new Dictionary<Item, GameObject>();
    private int currentItemIndex = 0;

    private List<Paper> paperOrder = new List<Paper>();
    private Dictionary<Paper, TextMeshProUGUI> pN = new Dictionary<Paper, TextMeshProUGUI>();
    private Dictionary<Paper, GameObject> paperModels = new Dictionary<Paper, GameObject>();
    private int currentPaperIndex = 0;

    private bool isRot = false;

    public ScrollRect itemScrollRect;
    public ScrollRect paperScrollRect;

    public RectTransform temPos;
    private const float camDis = -800f;


    public void Start()
    {

        Ui1.enabled = false;
        Ui2.enabled = false;
        Ui3.enabled = false;
        Ui4.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;

    }




    public void Update()
    {


        hasMapped = player.GetComponent<PlayerHealth>().hasMap;
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(escC.enabled == true)
            {
                escC.enabled = false;
            }
            ToggleInventory("SMS");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (escC.enabled == true)
            {
                escC.enabled = false;
            }
            ToggleInventory("Item");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (escC.enabled == true)
            {
                escC.enabled = false;
            }
            ToggleInventory("Paper");
        }
        else if (Input.GetKeyDown(KeyCode.F4) && hasMapped == false)
        {
            NullMap();
        }
        else if (Input.GetKeyDown(KeyCode.F4) && hasMapped == true)
        {
            if (escC.enabled == true)
            {
                escC.enabled = false;
            }
            ToggleInventory("Map");
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CloseCurrentInventory();
        }


        if (Ui2.enabled == true)
        {
            Ui2On();
        }

        if (Ui3.enabled == true)
        {
            Ui3On();
        }



    }
    void ToggleInventory(string inventoryType)
    {
        switch (inventoryType)
        {
            case "SMS":
                smss.SMSC();
                ToggleInventoryUI(Ui1);
                break;
            case "Item":
                ToggleInventoryUI(Ui2);
                SelectItem(1);
                break;
            case "Paper":
                ToggleInventoryUI(Ui3);
                SelectPaper(1);
                break;
            case "Map":
                ToggleInventoryUI(Ui4);
                break;
        }


    }

    void ToggleInventoryUI(Canvas inventoryUI)
    {
        if (inventoryUI.enabled == true)
        {
            CloseCurrentInventory();
        }
        else if (inventoryUI.enabled == false)
        {
            if (currentInventory != null)
            {
                currentInventory.enabled = false;
            }
            currentInventory = inventoryUI;
            inventoryUI.enabled = true;
            SelectItem(0);
            SelectPaper(0);

        }
        OpenMenu();
    }

    void CloseCurrentInventory()
    {
        if (currentInventory != null)
        {
            currentInventory.enabled = false;
        }
        CloseMenu();
    }

    public void AddItemToInventory(Item item)
    {
        if (item != null)
        {
            itemInventory.Add(item);
            itemOrder.Add(item);
            if (!itemIcons.ContainsKey(item))
            {
                UpdateItemInventoryUI();
            }

        }
    }

    public void AddPaperToInventory(Paper paper)
    {
        if (paper != null)
        {
            paperInventory.Add(paper);
            paperOrder.Add(paper);
            if (!pN.ContainsKey(paper))
            {
                UpdatePaperInventoryUI();
            }

        }
    }
    public void UpdateItemInventoryUI()
    {
        foreach (Item item in itemOrder)
        {
            if (!itemIcons.ContainsKey(item))
            {
                GameObject newItemSlot = Instantiate(itemNoneSelectPrefab, itemIconParent);
                newItemSlot.SetActive(true);
                Image iItemIcon = newItemSlot.AddComponent<Image>();
                iItemIcon.sprite = item.iconImage;
                itemIcons[item] = iItemIcon;
            }
        }
    }

    public void UpdatePaperInventoryUI()
    {
        foreach (Paper paper in paperOrder)
        {
            if (!pN.ContainsKey(paper))
            {
                GameObject newPaperSlot = Instantiate(paperNoneSelectPrefab, paperIconParent);
                newPaperSlot.SetActive(true);
                TextMeshProUGUI pPaper = newPaperSlot.AddComponent<TextMeshProUGUI>();
                pPaper.text = paper.paperName;
                pN[paper] = pPaper;
            }
            
        }
    }




    public void RemoveItem(Item item)
    {
        if (itemIcons.ContainsKey(item))
        {
            Destroy(itemIcons[item].gameObject);
            itemIcons.Remove(item);
            itemOrder.Remove(item);
            if (currentItemIndex >= itemOrder.Count)
            {
                currentItemIndex = itemOrder.Count - 1;
            }
            if (itemOrder.Count > 0)
            {
                SelectItem(currentItemIndex);
            }

        }
    }


    void ShowItemDetail(Item item)
    {
        currentItemTitle.enabled = true;
        currentItemTitle.text = item.itemName;
        currentItemInfo.enabled = true;
        currentItemInfo.text = item.description;
        Debug.Log("Selected Item: " + item.itemName);
        Show3DItem(item);
    }

    void Show3DItem(Item item)
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
        if (itemT != null)
        {
            itemT.Release();
            itemT = null;
        }

        //itemT = new RenderTexture(512, 512, 16);
        //itemT.Create();
        //itemDisplay.texture = itemT;

        //CreateItemCam();
        Debug.Log("Showing Item: " + item.itemName);
        if (item.itemModel != null)
        {
            // 아이템 오브젝트 생성 및 위치 초기화
            currentItem = Instantiate(item.itemModel, itemBg);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localScale = Vector3.one; // 스케일 조정
            currentItem.transform.localRotation = Quaternion.identity;

            //icam.transform.localPosition = new Vector3(0, camDis, 0);
            //icam.transform.LookAt(currentItem.transform.position);

            //icam.targetTexture = itemT;
        }
        else
        {
            Debug.LogWarning("아이템에 할당된 3D 오브젝트가없음: " + item.itemName);
        }
    }


    void ShowPaperDetail(Paper paper)
    {
        currentPaperTitle.enabled = true;
        currentPaperTitle.text = paper.paperName;
        Debug.Log("골라진 종이: " + paper.paperName);
        Show3DPaper(paper);
    }

    void Show3DPaper(Paper paper)
    {
        if (currentPaper != null)
        {
            Destroy(currentPaper);
        }

        if (paperT != null)
        {
            paperT.Release();
            paperT = null;
        }

        Debug.Log("지금 보여지는 종이: " + paper.paperName);
        if (paper.paperModel != null)
        {
            // 종이 오브젝트 생성 및 위치 초기화
            currentPaper = Instantiate(paper.paperModel, paperBg);
            currentPaper.transform.localPosition = Vector3.zero;
            currentPaper.transform.localScale = Vector3.one; // 스케일 조정
            currentItem.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("할당된 3D 모델이 없습니다: " + paper.paperName);
        }
    }


    private void OnMouseDown()
    {
        isRot = true;
    }

    private void OnMouseUp()
    {
        isRot = false;
    }

    void UseItem()
    {
        if (currentItemIndex >= 0 && currentItemIndex < itemOrder.Count)
        {
            Item item = itemOrder[currentItemIndex];
            item.Use(); // 아이템의 Use 메서드 호출
            if (item.hasFun)
            {
                RemoveItem(item);
            }
        }
        else
        {
            Debug.LogError("현재 아이템 인덱스가 유효하지 않은거같은..??데요??");
        }
    }

    public void SelectPrevItem()
    {
        if (currentItemIndex >= 1)
        {
            currentItemIndex--;
            SelectItem(currentItemIndex);
        }
        return;
    }
    public void SPPItem()
    {
        if (itemSelectBG.rectTransform.anchoredPosition.y < 210)
        {
            itemSelectBG.rectTransform.anchoredPosition = new Vector3(-460, itemSelectBG.rectTransform.anchoredPosition.y + 190, 0);
        }
    }

    public void SelectPrevPaper()
    {
        if (currentItemIndex >= 1)
        {
            currentPaperIndex--;
            SelectPaper(currentPaperIndex);
        }
        return;
    }

    public void SPPaper()
    {
        if (paperSelectBG.rectTransform.anchoredPosition.y < 302)
        {
            paperSelectBG.rectTransform.anchoredPosition = new Vector3(550, paperSelectBG.rectTransform.anchoredPosition.y + 90, 0);
        }
    }

    public void SelectNextItem()
    {
        if (currentItemIndex < itemInventory.Count - 1)
        {
            currentItemIndex++;
            SelectItem(currentItemIndex);
        }
        return;
    }

    public void SNItem()
    {
        if (itemSelectBG.rectTransform.anchoredPosition.y > -360)
        {
            itemSelectBG.rectTransform.anchoredPosition = new Vector3(-460, itemSelectBG.rectTransform.anchoredPosition.y - 190, 0);
        }
        return;
    }

    public void SelectNextPaper()
    {
        if (currentPaperIndex < paperInventory.Count - 1)
        {
            currentPaperIndex++;
            SelectPaper(currentPaperIndex);
        }
        return;
    }

    public void SNPaper()
    {
        if (paperSelectBG.rectTransform.anchoredPosition.y > -418)
        {
            paperSelectBG.rectTransform.anchoredPosition = new Vector3(550, paperSelectBG.rectTransform.anchoredPosition.y - 90, 0);
        }
        return;
    }

    void SelectItem(int index)
    {
        Item item = itemOrder[index];
        currentItemIndex = index;
        ShowItemDetail(item);

    }

    void SelectPaper(int index)
    {
        Paper paper = paperOrder[index];
        currentPaperIndex = index;
        ShowPaperDetail(paper);
    }

    public void Ui2On()
    {
        if (isRot == true)
        {
            float rotSpeed = 100f * Time.unscaledDeltaTime;
            float rotX = Input.GetAxis("Mouse X") * rotSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed;
            currentItem.transform.Rotate(Vector3.up * rotX);
            currentItem.transform.Rotate(Vector3.left * rotY);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectPrevItem();
            SPPItem();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextItem();
            SNItem();
        }
    }

    public void Ui3On()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectPrevPaper();
            SPPaper();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextPaper();
            SNPaper();
        }

    }

    public void OpenMenu()
    {
        AudioSource.PlayClipAtPoint(mnOpen, player.transform.position);
    }

    public void CloseMenu()
    {
        AudioSource.PlayClipAtPoint(mnClose, player.transform.position);
    }

    public void NullMap()
    {
        AudioSource.PlayClipAtPoint(nope, player.transform.position);
    }

}
