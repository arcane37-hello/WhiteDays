using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{
    public PlayerHealth ph;
    // �ʱ� ������ ��
    private Vector3 initialScale = new Vector3(0.4f, 0.6f, 0.4f);

    // ������ ������ ��
    private Vector3 scaledDown = new Vector3(0.15f, 0.225f, 0.15f);

    // ���� ������ ����
    private Vector3 targetScale;

    private void Start()
    {
        // �ʱ� ������ ����
        transform.localScale = initialScale;
        targetScale = initialScale;
    }

    public void Update()
    {
        
        // ���� ��Ʈ�� Ű�� ���ȴ��� üũ
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (ph.GetComponent<PlayerHealth>().inVent == false)
            ScaledDown();
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5f);
        }
        else
        {
            if (ph.GetComponent<PlayerHealth>().inVent == false)
            ScaledUp();
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5f);
        }
        if(ph.inVent == true)
        {
            ScaledDown();
            transform.localScale = targetScale;
        }    

        // �������� ����
        
    }

    public void ScaledDown()
    {
        targetScale = scaledDown;
    }
    public void ScaledUp()
    {
        targetScale = initialScale;
    }
}