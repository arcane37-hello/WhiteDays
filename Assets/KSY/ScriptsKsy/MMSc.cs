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
    public TMP_FontAsset font;
    public Color textColor = Color.white;
    public int fontSize = 30;

    public Canvas escC;
    public SMSSc smss;
    public GameObject icamPrefab;
    public Camera icam;
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
    public AudioClip chooseItem;
    public AudioClip choosePaper;
    public AudioClip getItem;
    public AudioClip getPaper;

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
    public Image currentPaper;

    public Transform itemBg;

    public Image itemIcon;
    public TextMeshProUGUI paperName;

    public Canvas currentInventory;

    public List<Item> itemOrder = new List<Item>();
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

    public float camDis = 0.3f;

    public GameObject itemViewParent;


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
                break;
            case "Paper":
                ToggleInventoryUI(Ui3);
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

        }

        SelectItem(0);
        SelectPaper(0);
        paperSelectBG.rectTransform.anchoredPosition = new Vector3(550, 302, 0);
        itemSelectBG.rectTransform.anchoredPosition = new Vector3(-460, 210, 0);
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
                pPaper.rectTransform.sizeDelta = new Vector2(400, 80);
                pPaper.color = textColor;
                pPaper.font = font;
                pPaper.enableAutoSizing = false;
                pPaper.fontSize = fontSize;
                pPaper.alignment = TextAlignmentOptions.Center;
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
        icam.depth = 30;
        Debug.Log("Showing Item: " + item.itemName);
        icam = item.itemCamera;
        icam.depth = 50;
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
        Debug.Log("지금 보여지는 종이: " + paper.paperName);
        if (paper.paperModel != null)
        {
            currentPaper.sprite = paper.paperModel;
        }
        else
        {
            Debug.LogWarning("할당된 이미지가 없습니다: " + paper.paperName);
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
            ChooseItem();
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
            ChoosePaper();
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
            ChooseItem();
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
            ChoosePaper();
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
        if(Ui2.enabled == true)
        {
            ShowItemDetail(item);
        }
    }

    void SelectPaper(int index)
    {
        Paper paper = paperOrder[index];
        currentPaperIndex = index;
        if(Ui3.enabled == true)
        {
            ShowPaperDetail(paper);
        }
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

    public void ChooseItem()
    {
        AudioSource.PlayClipAtPoint(chooseItem, player.transform.position);
    }

    public void ChoosePaper()
    {
        AudioSource.PlayClipAtPoint(choosePaper, player.transform.position);
    }

    public void GetItem()
    {
        AudioSource.PlayClipAtPoint(getItem, player.transform.position);
    }

    public void GetPaper()
    {
        AudioSource.PlayClipAtPoint(getPaper, player.transform.position);
    }

}
