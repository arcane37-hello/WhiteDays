using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool isDragging = false; // ��ٸ��� �巡�� ������ ����
    private Vector3 offset; // ��ٸ��� ���콺 ������ �Ÿ�
    private Camera mainCamera; // ���� ī�޶� ����
    public bool can = false;
    public GameObject hm;
    public float shortDis = 1;
    public float dis;
    public GameObject pos;

    private void Start()
    {
        // ���� ī�޶� �ڵ����� �Ҵ�
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned.");
        }
    }

    public void Update()
    {
        dis = Vector3.Distance(hm.transform.position, gameObject.transform.position);
    }

    private void OnMouseDown()
    {
        if (mainCamera == null) return;
        
        if(can == false && dis < shortDis)
        {
            // ��ٸ��� ���콺 Ŭ�� �� �巡�� ����
            isDragging = true;
            // ��ٸ��� ���� ��ġ�� ���콺 ��ġ ������ �������� ���
            offset = transform.position - GetMouseWorldPosition();
        }
        else if(can == true && dis < shortDis)
        {
            GameObject pos2 = Instantiate(pos);
            pos2.SetActive(true);
            Vector3 position = pos2.GetComponent<Transform>().position;
            hm.GetComponent<Collider>().enabled = false;
            hm.GetComponent<CharacterController>().enabled = false;
            hm.transform.position = position;
            Destroy(pos2 );
            hm.GetComponent<CharacterController>().enabled = true;
            hm.GetComponent<Collider>().enabled = true;
            return;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // ��ٸ��� �巡�� ���� �� ���콺�� ���� ��ġ�� ��ٸ� ��ġ�� ������Ʈ
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        // ���콺 ��ư�� ���� �巡�� ����
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) return Vector3.zero;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane; // ī�޶��� near clip plane�� ����Ͽ� Z �� �� ����
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
            // �浹 �� ��ٸ��� �ӵ��� ���ҽ�Ű�ų� ó��
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // �浹 �� �ӵ��� 0���� ����
            }

    }


}