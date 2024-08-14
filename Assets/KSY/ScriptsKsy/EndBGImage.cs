using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBGImage : MonoBehaviour
{

    public float rotSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);
    }
}
