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
    private GameObject currentItemSelectBG; // 현재 활성화된 itemSelectBG
    private GameObject currentPaperSelectBG;

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
    private int currentItemIndex = -1;

    private List<Paper> paperOrder = new List<Paper>();
    private Dictionary<Paper, TextMeshProUGUI> pN = new Dictionary<Paper, TextMeshProUGUI>();
    private Dictionary<Paper, GameObject> paperModels = new Dictionary<Paper, GameObject>();
    private int currentPaperIndex = -1;

    private bool isRot = false;

    public ScrollRect itemScrollRect;
    public ScrollRect paperScrollRect;

    public RectTransform temPos;
    private const float camDis = -800f;

    private bool canMove = true;

    public void Start()
    {

        Ui1.enabled = false;
        Ui2.enabled = false;
        Ui3.enabled = false;
        Ui4.enabled = false;


    }

    //private void CreateItemCam()
    //{
    //    if(icam != null)
    //    {
    //        Destroy(gameObject);
    //        CreateItemCam();
    //    }
    //    GameObject camObj = Instantiate(icamPrefab, itemBg);
    //    icam = camObj.GetComponent<Camera>();
    //    // 카메라 설정
    //    icam.orthographic = false; // Perspective 모드로 설정
    //    icam.fieldOfView = 60; // 시야각 설정
    //}
    //private void CreatePaperCam()
    //{
    //    if(icam != null)
    //    {
    //        Destroy(gameObject);
    //        CreatePaperCam();
    //    }
    //    GameObject camObj = Instantiate(icamPrefab, paperBg);
    //    icam = camObj.GetComponent<Camera>();
    //    // 카메라 설정
    //    icam.orthographic = false; // Perspective 모드로 설정
    //    icam.fieldOfView = 60; // 필드 오브 뷰를 설정
    //}




    public void Update()
    {


        hasMapped = player.GetComponent<PlayerHealth>().hasMap;
        if (Input.GetKeyDown(KeyCode.F1))
        {

            ToggleInventory("SMS");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {

            ToggleInventory("Item");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {

            ToggleInventory("Paper");
        }
        else if (Input.GetKeyDown(KeyCode.F4) && hasMapped == true)
        {

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
        if (currentInventory == inventoryUI)
        {
            CloseCurrentInventory();
        }
        else
        {
            if (currentInventory != null)
            {
                currentInventory.enabled = false;
            }
            currentInventory = inventoryUI;
            inventoryUI.enabled = true;

        }
    }

    void CloseCurrentInventory()
    {
        if (currentInventory != null)
        {
            currentInventory.enabled = false;
        }
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
    private void UpdateItemInventoryUI()
    {
        // 현재 아이템 UI 요소를 교체할 때 기존 UI 요소를 먼저 제거합니다.
        CleanUpItemIcons();
        foreach (Item item in itemOrder)
        {
            if (!itemIcons.ContainsKey(item))
            {
                // 새로운 아이템 아이콘 생성
                Image iItemIcon = new GameObject("ItemIcon").AddComponent<Image>();
                iItemIcon.transform.SetParent(itemIconParent, false); // itemIconParent의 자식으로 설정

                // 아이템의 스프라이트를 설정합니다.
                iItemIcon.sprite = item.iconImage;

                // 아이콘의 크기와 위치를 조절할 필요가 있을 수 있습니다.
                RectTransform rectTransform = iItemIcon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(100, 100); // 예시로 크기 설정
                rectTransform.anchoredPosition = new Vector2(0, 0); // 예시로 위치 설정

                // 클릭 이벤트 추가
                Button iconButton = iItemIcon.gameObject.AddComponent<Button>();
                iconButton.onClick.AddListener(() => OnItemIconClicked(item));

                // 아이템과 해당 아이콘의 연결을 저장합니다.
                itemIcons[item] = iItemIcon;
            }
        }

        // 기본적으로 가장 위에 있는 아이템을 선택합니다.
        if (itemOrder.Count > 0)
        {
            SelectItem(0); // 가장 위에 있는 아이템을 선택
        }
    }
    private void OnItemIconClicked(Item item)
    {
        int index = itemOrder.IndexOf(item);
        if (index >= 0)
        {
            SelectItem(index);
        }
    }

    private void UpdatePaperInventoryUI()
    {
        // 현재 종이 UI 요소를 교체할 때 기존 UI 요소를 먼저 제거합니다.
        CleanUpPaperIcons();
        // 새 종이 UI 요소를 생성합니다.
        foreach (Paper paper in paperOrder)
        {
            // 종이가 이미 UI 요소를 가지고 있는지 확인합니다.
            if (!pN.ContainsKey(paper))
            {
                // 새로운 종이 텍스트 생성
                TextMeshProUGUI paperText = new GameObject("PaperText").AddComponent<TextMeshProUGUI>();
                paperText.transform.SetParent(paperIconParent, false); // paperIconParent의 자식으로 설정

                // 종이의 텍스트를 설정합니다.
                paperText.text = paper.paperName;

                // 텍스트의 크기와 위치를 조절할 필요가 있을 수 있습니다.
                RectTransform rectTransform = paperText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(200, 50); // 예시로 크기 설정
                rectTransform.anchoredPosition = new Vector2(0, 0); // 예시로 위치 설정

                // 클릭 이벤트 추가
                Button iconButton = paperText.gameObject.AddComponent<Button>();
                iconButton.onClick.AddListener(() => OnPaperIconClicked(paper));

                // 종이와 해당 텍스트의 연결을 저장합니다.
                pN[paper] = paperText;
            }
            else
            {
                // 이미 존재하는 경우, 텍스트만 업데이트합니다.
                pN[paper].text = paper.paperName;
            }
        }

        // 기본적으로 가장 위에 있는 종이를 선택합니다.
        if (paperOrder.Count > 0)
        {
            SelectPaper(0); // 가장 위에 있는 종이를 선택
        }
    }
    private void OnPaperIconClicked(Paper paper)
    {
        int index = paperOrder.IndexOf(paper);
        if (index >= 0)
        {
            SelectPaper(index);
        }
    }

    private void CleanUpItemIcons()
    {
        foreach (var icon in itemIcons.Values)
        {
            Destroy(icon);
        }
        itemIcons.Clear();
    }

    private void CleanUpPaperIcons()
    {
        foreach (var icon in pN.Values)
        {
            Destroy(icon.gameObject);
        }
        pN.Clear();
    }


    public void Scroll(float scrollAmount)
    {
        if (Ui2 == true)
        {
            itemScrollRect.verticalNormalizedPosition += scrollAmount;
        }
        if (Ui3 == true)
        {
            paperScrollRect.verticalNormalizedPosition += scrollAmount;
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
    public void RemovePaper(Paper paper)
    {
        if (pN.ContainsKey(paper))
        {
            // 텍스트 UI를 제거하고
            Destroy(pN[paper].gameObject);
            pN.Remove(paper);
            paperOrder.Remove(paper);

            // 현재 종이 인덱스 업데이트
            if (currentPaperIndex >= paperOrder.Count)
            {
                currentPaperIndex = paperOrder.Count - 1;
            }

            // 선택된 종이 업데이트
            if (paperOrder.Count > 0)
            {
                SelectPaper(currentPaperIndex);
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

        //paperT = new RenderTexture(512, 512, 0);
        //paperT.Create();
        //paperDisplay.texture = paperT;

        //CreatePaperCam();
        Debug.Log("지금 보여지는 종이: " + paper.paperName);
        if (paper.paperModel != null)
        {
            // 종이 오브젝트 생성 및 위치 초기화
            currentPaper = Instantiate(paper.paperModel, paperBg);
            currentPaper.transform.localPosition = Vector3.zero;
            currentPaper.transform.localScale = Vector3.one; // 스케일 조정
            currentItem.transform.localRotation = Quaternion.identity;

            //icam.transform.localPosition = new Vector3(0, camDis, 0);
            //icam.transform.LookAt(currentPaper.transform.position);

            //icam.targetTexture = paperT;
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
        if (itemOrder.Count == 1 || currentItemIndex <= 0)
        {
            return;
        }

        currentItemIndex--;
        SelectItem(currentItemIndex);
    }

    public void SelectPrevPaper()
    {
        if (paperOrder.Count == 1 || currentPaperIndex <= 0)
        {
            return;
        }

        currentPaperIndex--;
        SelectPaper(currentPaperIndex);
    }

    public void SelectNextItem()
    {
        if (currentItemIndex >= itemOrder.Count)
        {
            return;
        }

        currentItemIndex++;
        SelectItem(currentItemIndex);
    }

    public void SelectNextPaper()
    {
        if (currentPaperIndex >= paperOrder.Count)
        {
            return;
        }

        currentPaperIndex++;
        SelectPaper(currentPaperIndex);
    }

    void SelectItem(int index)
    {
        // 이전 선택 상태에서 선택 배경이 있으면 제거합니다.
        if (currentItemSelectBG != null)
        {
            Destroy(currentItemSelectBG);
            if (itemIcons.TryGetValue(itemOrder[currentItemIndex], out var icon))
            {
                // 아이콘을 원래 부모로 복원하고, 위치를 중앙으로 설정
                icon.transform.SetParent(itemIconParent, false);
                icon.rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        Item item = itemOrder[index];
        ShowItemDetail(item);

        // 현재 선택된 아이템의 아이콘 가져오기
        Image selectedItemIcon = itemIcons[item];

        // 선택 배경을 content의 자식으로 생성
        //itemSelectBG.transform.SetParent(itemIconParent.transform, false);
       // RectTransform selectBGRectTransform = itemSelectBG.GetComponent<RectTransform>();
        // 선택 배경 활성화
        //itemSelectBG.enabled = true;
        // 선택 배경을 아이콘의 자식으로 만들기 전에 위치 설정
        //selectBGRectTransform.anchoredPosition = Vector2.zero;

        // 선택 배경을 아이콘의 위에 위치시키기 위해 아이콘을 선택 배경의 자식으로 설정
        selectedItemIcon.transform.SetParent(itemSelectBG.transform, false);

        // 아이콘의 위치를 선택 배경의 중앙으로 설정
        selectedItemIcon.rectTransform.anchoredPosition = Vector2.zero;



        // 현재 선택된 아이템 인덱스 업데이트
        currentItemIndex = index;
    }

    void SelectPaper(int index)
    {
        // 이전 선택 상태에서 선택 배경이 있으면 제거합니다.
        if (currentPaperSelectBG != null)
        {
            Destroy(currentPaperSelectBG);
            if (pN.TryGetValue(paperOrder[currentPaperIndex], out var text))
            {
                // 텍스트를 원래 부모로 복원하고, 위치를 중앙으로 설정
                text.transform.SetParent(paperIconParent, false);
                text.rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        Paper paper = paperOrder[index];
        ShowPaperDetail(paper);

        // 현재 선택된 종이의 텍스트 가져오기
        TextMeshProUGUI selectedPaperN = pN[paper];

        // 선택 배경을 content의 자식으로 생성
        currentPaperSelectBG = Instantiate(paperSelectBG.gameObject, paperIconParent);
        RectTransform selectBGRectTransform = currentPaperSelectBG.GetComponent<RectTransform>();
        // 선택 배경을 텍스트의 자식으로 만들기 전에 위치 설정
        selectBGRectTransform.anchoredPosition = Vector2.zero;

        // 선택 배경 활성화
        currentPaperSelectBG.SetActive(true);

        // 선택 배경을 텍스트의 위에 위치시키기 위해 텍스트를 선택 배경의 자식으로 설정
        selectedPaperN.transform.SetParent(currentPaperSelectBG.transform, false);

        // 텍스트의 위치를 선택 배경의 중앙으로 설정
        selectedPaperN.rectTransform.anchoredPosition = Vector2.zero;



        // 현재 선택된 종이 인덱스 업데이트
        currentPaperIndex = index;
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
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextItem();
        }
    }

    public void Ui3On()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectPrevPaper();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextPaper();
        }

    }

}
