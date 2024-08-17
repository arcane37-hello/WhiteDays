using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoloText : MonoBehaviour
{
    public Canvas honCan;
    public TextMeshProUGUI honText;
    public GameObject hm;
    public AudioClip hon;

    void Start()
    {
        honCan.enabled = false;
    }

    void Update()
    {
        
    }

    public void Hon1()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "소영이는 2학년 8반이다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon2()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "책꽂이에는 교지가 배부되어 있다.";
        Destroy(honCan2.gameObject, 3);
    }
    public void Hon3()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "지도를 획득했다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon4()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "아이템을 획득했다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon5()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "쪽지를 획득했다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon6()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text =  "문이 잠겨있다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon7()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "지금은 나갈 수 없다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon8()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "선물을 전해주었다.";
        Destroy(honCan2.gameObject, 2);
    }
    public void Hon9()
    {
        Canvas honCan2 = Instantiate(honCan);
        honCan2.enabled = true;
        honCan.enabled = false;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position, 0.7f);
        honCan2.GetComponentInChildren<Image>().GetComponentInChildren<TextMeshProUGUI>().text = "이 자리가 아니다.";
        Destroy(honCan2.gameObject, 2);
    }
}
