using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ventAnim : MonoBehaviour
{
    public Canvas vpCanvas;
    public VideoPlayer vp;
    public Collider ventAnimCollider;
    public PlayerMove pm;


    private void Awake()
    {
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
        if(other == ventAnimCollider)
        {
            vpCanvas.enabled = true;
            Destroy(ventAnimCollider);
            vp.Play();
            pm.canMove = false;
            pm.canRot = false;
        }
    }

    private void OnVideoEnd(VideoPlayer vv)
    {
        vp.Stop();
        vpCanvas.enabled = false;
        pm.canMove = true;
        pm.canRot = true;
    }
}
