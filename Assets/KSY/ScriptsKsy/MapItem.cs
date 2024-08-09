using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public float dist = 0.5f;
    public GameObject map;
    private GameObject hm;
    private PlayerHealth player;

    void Start()
    {
        hm = GameObject.FindGameObjectWithTag("Player");
        if(hm != null)
        {
            player = hm.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (hm != null && player != null)
        {
            float distToP = Vector3.Distance(transform.position, hm.transform.position);
            if(distToP <= dist)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    player.GetMap();
                    Destroy(map);
                }
            }
        }    
    }

   
}
