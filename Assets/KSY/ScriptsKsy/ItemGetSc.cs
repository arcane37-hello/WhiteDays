using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetSc : MonoBehaviour
{
    public float pickRange = 1.5f;

    public MMSc mmsc;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            TryPickupItem();
        }
    }

    void TryPickupItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickRange);
        foreach(var hitCollider in hitColliders)
        {

            Item item = hitCollider.GetComponent<Item>();
            if (item != null)
            {

                mmsc.AddItemToInventory(item);
                break;

            }
            Paper paper = hitCollider.GetComponent<Paper>();
            if (paper != null)
            {
                mmsc.AddPaperToInventory(paper);
                break;
            }
        }
    }
}
