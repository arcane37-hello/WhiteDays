using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ventAnim : MonoBehaviour
{
    public Canvas vpCanvas;
    public VideoPlayer vp;
    public GameObject hm;
    public PlayerMove pm;

    void Start()
    {
        vp.loopPointReached += OnVideoEnd;
        vpCanvas.enabled = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == hm)
        {
            vp.Play();
            //pm.canMove = false;
            //pm.canRot = false;
            Destroy(gameObject);
        }
    }

    private void OnVideoEnd(VideoPlayer vv)
    {
        vpCanvas.enabled = false;
        //pm.canMove = true;
        //pm.canRot = true;
    }
}
