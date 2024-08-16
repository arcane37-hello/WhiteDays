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

    public IEnumerator Hon1()
    {
        honCan.enabled = true;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        honText.text = "소영이는 2학년 8반이다.";
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon2()
    {
        honCan.enabled = true;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        honText.text = "내 반이다.";
        yield return new WaitForSeconds(2);
        honText.text = "책꽂이에는 교지가 배부되어 있다.";
        yield return new WaitForSeconds(3);
        honText.text = "교지에 수록된 지도가 도움이 될 것 같다.";
        yield return new WaitForSeconds(3.5f);
        honCan.enabled = false;
    }
    public IEnumerator Hon3()
    {
        honCan.enabled = true;
        honText.text = "지도를 획득했다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon4()
    {
        honCan.enabled = true;
        honText.text = "아이템을 획득했다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon5()
    {
        honCan.enabled = true;
        honText.text = "쪽지를 획득했다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon6()
    {
        honCan.enabled = true;
        honText.text = "문이 잠겨있다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon7()
    {
        honCan.enabled = true;
        honText.text = "지금은 나갈 수 없다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon8()
    {
        honCan.enabled = true;
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        honText.text = "선물을 전해주었다.";
        yield return new WaitForSeconds(2);
        honText.text = "학교에서 나가자.";
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
    public IEnumerator Hon9()
    {
        honCan.enabled = true;
        honText.text = "이 자리가 아니다.";
        AudioSource.PlayClipAtPoint(hon, hm.transform.position);
        yield return new WaitForSeconds(2);
        honCan.enabled = false;
    }
}
