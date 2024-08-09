using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MMSc : MonoBehaviour
{
    private List<Item> itemInventory = new List<Item>();
    private List<Paper> paperInventory = new List<Paper>();

    public Transform zipper;
    public float iconRange = 100f;
    public RectTransform zipRT;

    public GameObject player;

    public AudioClip mnOpen;
    public AudioClip mnClose;
    public AudioClip nope;

    private bool isPaused = false;
    private bool hasMapped;

    public Camera mCam;

    public Canvas Ui1;
    public Canvas Ui2;
    public Canvas Ui3;
    public Canvas Ui4;

    public Image itemSelectBG;

    public Transform itemIconParent;
    public Transform paperIconParent;

    public GameObject itemPrefab;
    public GameObject paperPrefab;
    public GameObject mapPrefab;

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

    private List<string> itemOrder = new List<string>();
    private Dictionary<string, Image> itemIcons = new Dictionary<string, Image>();
    private Dictionary<string, GameObject> itemModels = new Dictionary<string, GameObject>();
    private int currentItemIndex = -1;

    private List<string> paperOrder = new List<string>();
    private Dictionary<string, TextMeshProUGUI> paperIcons = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, GameObject> paperModels = new Dictionary<string, GameObject>();
    private int currentPaperIndex = -1;

    private bool isRot = false;
    private bool isChasing;

    private bool inputLocked = false;

    public ScrollRect itemScrollRect;
    public ScrollRect paperScrollRect;

    public RectTransform temPos;
    private const float visibleRangeOffset = 400f;

    public void Start()
    {
        Ui1.enabled = false;
        Ui2.enabled = false;
        Ui3.enabled = false;
        Ui4.enabled = false;

        
    }

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

        if (Ui2.enabled == true && isRot == true)
        {
            float rotSpeed = 100f * Time.unscaledDeltaTime;
            float rotX = Input.GetAxis("Mouse X") * rotSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed;
            itemPrefab.transform.Rotate(Vector3.up * rotX);
            itemPrefab.transform.Rotate(Vector3.left * rotY);
        }
        if(Ui2.enabled == true && Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
        if(Ui2.enabled ==true && Input.GetKeyDown(KeyCode.Q) && !inputLocked)
        {
            SelectPrevItem();
        }
        if (Ui2.enabled == true && Input.GetKeyDown(KeyCode.E) && !inputLocked)
        {
            SelectNextItem();
        }


        if (Ui3.enabled ==true && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollSpeed = 30f * Time.unscaledDeltaTime;
            float scrollP = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            paperPrefab.transform.TransformVector(Vector3.up * scrollP);
            if(paperPrefab.transform.position.y >= 30)
            {
                Vector3 pPy = paperPrefab.transform.position;
                pPy.y = 30;
            }
            else if (paperPrefab.transform.position.y <= -30)
            {
                Vector3 pPy = paperPrefab.transform.position;
                pPy.y = 30;
            }
        }
        
        UpdateItemVisibility();
        UpdatePaperVisibility();

    }
    void ToggleInventory(string inventoryType)
    {
        switch (inventoryType)
        {
            case "SMS":
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
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

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
            inventoryUI.enabled = true ;
            Time.timeScale = 0f;
        }
    }

    void CloseCurrentInventory()
    {
        if (currentInventory != null)
        {
            currentInventory.enabled = false;
            currentInventory = null;
            Time.timeScale = 1f;
        }
    }

    public void AddItemToInventory(Item item)
    {
        if(item != null)
        {
            itemInventory.Add(item);
        }
    }

    public void AddPaperToInventory(Paper paper)
    {
        if (paper != null)
        {
            paperInventory.Add(paper);
            UpdatePaperInventoryUI();
        }
    }
    private void UpdateItemInventoryUI()
    {
        CleanUpItemIcons();
        for(int i = 0; i < itemInventory.Count; i++)
        {
            Item item = itemInventory[i];
            string itemId = item.itemId;
            Image iItemIcon = Instantiate(itemIcon, zipper);
            iItemIcon.GetComponent<Image>().sprite = GetItemIcon(itemId);
            RectTransform iconRT = iItemIcon.GetComponent<RectTransform>();
            iconRT.anchoredPosition = new Vector2(0, -i * iconRange);
            itemIcons[itemId] = iItemIcon;
        }
        float contentH = itemInventory.Count * iconRange;
        zipRT.sizeDelta = new Vector2(zipRT.sizeDelta.x, contentH);
        ClampIconPositions();
    }

    private void UpdatePaperInventoryUI()
    {
        CleanUpPaperIcons();
        for (int i = 0; i < paperInventory.Count; i++)
        {
            Paper paper = paperInventory[i];
            string paperId = paper.paperId;
            TextMeshProUGUI paperN = Instantiate(paperName, zipper);
            paperN.GetComponent<TextMeshProUGUI>().text = GetPaperName(paperId);
            RectTransform nameRectTransform = paperN.GetComponent<RectTransform>();
            nameRectTransform.anchoredPosition = new Vector2(0, -i * iconRange);
            paperIcons[paperId] = paperN;
        }
        float contentH = paperInventory.Count * iconRange;
        zipRT.sizeDelta = new Vector2(zipRT.sizeDelta.x, contentH);
        ClampIconPositions();
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
        foreach (var icon in paperIcons.Values)
        {
            Destroy(icon);
        }
        paperIcons.Clear();
    }

    private string GetPaperName(string paperId)
    {
        return paperId;
    }

    Sprite GetItemIcon(string itemId)
    {
        return Resources.Load<Sprite>("경로/프리팹이름" + itemId);
    }


    private void ClampIconPositions()
    {
        foreach(var icon in itemIcons.Values)
        {
            RectTransform iconRectTransform = icon.GetComponent<RectTransform>();
            if(iconRectTransform.anchoredPosition.y < -zipRT.rect.height || iconRectTransform.anchoredPosition.y > zipRT.rect.height)
            {
                icon.enabled = false;
            }
            else
            {
                icon.enabled = true;
            }
        }
    }

    public void Scroll(float scrollAmount)
    {
        zipper.localPosition += new Vector3(0, scrollAmount, 0);
        ClampIconPositions();
    }

    public void RemoveItem(string itemId)
    {
        if(itemIcons.ContainsKey(itemId))
        {
            Destroy(itemIcons[itemId]);
            itemIcons.Remove(itemId);
            itemOrder.Remove(itemId);
            if(currentItemIndex >= itemOrder.Count)
            {
                currentItemIndex = itemOrder.Count - 1;
            }
            SelectItem(currentItemIndex);
        }
    }



    void ShowItemDetail(string itemId)
    {
        currentItemTitle.enabled = true;
        currentItemInfo.enabled = true;
        Debug.Log("Selected Item: " + itemId);
        Show3DItem(itemId);
    }

    void Show3DItem(string itemId)
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
        Debug.Log("Showing Item: " + itemId);
        currentItem = Instantiate(itemPrefab, itemBg);
        currentItem.transform.localPosition = Vector3.zero;
    }

    void ShowPaperDetail(string paperId)
    {
        currentPaperTitle.enabled = true;
        Debug.Log("Selected paper: " + paperId);
        Show3DPaper(paperId);
    }

    void Show3DPaper(string paperId)
    {
        if(currentPaper != null)
        {
            Destroy(currentPaper);
        }
        currentPaper = Instantiate(paperPrefab, paperBg);
        currentPaper.transform.localPosition = Vector3.zero;
        
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
        // 만약 현재 아이템에게 할당된 능력이 있다면,
        // 할당된 능력을 사용한다.
    }

    public void SelectPrevItem()
    {
        if (itemOrder.Count == 0 || currentItemIndex <= 0)
        {
            StartCoroutine(NoneMove());
            return;
        }

        currentItemIndex--;
        SelectItem(currentItemIndex);
    }

    public void SelectNextItem()
    {
        if (itemOrder.Count == 0 || currentItemIndex >= itemOrder.Count - 1)
        {
            StartCoroutine(NoneMove());
            return;
        }

        currentItemIndex++;
        SelectItem(currentItemIndex);
    }

    void SelectItem(int index)
    {
        if (index < 0 || index >= itemOrder.Count) return;
        string itemId = itemOrder[index];
        ShowItemDetail(itemId);
        Show3DItem(itemId);
        Image selectedItem = itemIcons[itemId];
        itemSelectBG.enabled = true;
        itemSelectBG.transform.SetParent(selectedItem.transform, false);
        itemSelectBG.rectTransform.anchoredPosition = Vector2.zero;
        itemSelectBG.rectTransform.SetAsFirstSibling();
    }

    IEnumerator NoneMove()
    {
        inputLocked = true;
        AudioSource.PlayClipAtPoint(nope, mCam.transform.position);
        Vector3 oP = itemSelectBG.rectTransform.anchoredPosition;
        float shakeDuration = 0.3f;
        float shakeMagnitude = 10f;
        float elapsed = 0;

        while(elapsed < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            itemSelectBG.rectTransform.anchoredPosition = new Vector3(x, y, oP.z);
            elapsed += Time.deltaTime;
            yield return null;

        }

        itemSelectBG.rectTransform.anchoredPosition = oP;
        inputLocked = false;
    }

    void UpdateItemVisibility()
    {
        float referenceY = temPos.anchoredPosition.y;
        foreach(var kvp in itemIcons)
        {
            Image icon = kvp.Value;
            float yPos = icon.rectTransform.anchoredPosition.y;
            icon.gameObject.SetActive(Mathf.Abs(yPos - referenceY) <= visibleRangeOffset);
        }
    }

    void UpdatePaperVisibility()
    {
        float referenceY = temPos.anchoredPosition.y;
        foreach (var kvp in paperIcons)
        {
            TextMeshProUGUI icon = kvp.Value;
            float yPos = icon.rectTransform.anchoredPosition.y;
            icon.gameObject.SetActive(Mathf.Abs(yPos - referenceY) <= visibleRangeOffset);
        }
    }



    
}
