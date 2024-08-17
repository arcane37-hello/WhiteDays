using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoloTextTrigger : MonoBehaviour
{
    public SoloText sT;
    public Collider hon1;
    public Collider hon2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == hon1)
        {
            sT.Hon1();
            Destroy(hon1);
        }
        else if (other == hon2)
        {
            sT.Hon2();
            Destroy(hon2);
        }
    }


}
