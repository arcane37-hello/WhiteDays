using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ventAnim : MonoBehaviour
{
    public Canvas vpCanvas;
    public GameObject vpOb;
    public VideoPlayer vp;
    public GameObject hm;
    public PlayerMove pm;


    private void Awake()
    {
        vpOb.SetActive(false);
        vpCanvas.enabled = false;
    }
    void Start()
    {
        vp.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == hm)
        {
            vpOb.SetActive(true);
            vpCanvas.enabled = true;
            vp.Play();
            //pm.canMove = false;
            //pm.canRot = false;
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }

    private void OnVideoEnd(VideoPlayer vv)
    {
        vp.Stop();
        vpOb.SetActive(false);
        vpCanvas.enabled = false;
        //pm.canMove = true;
        //pm.canRot = true;
    }
}
