using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSc : MonoBehaviour
{
    public Sit sit;
    public Collider vent;
    public PlayerHealth ph;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == vent)
        {
            ph.inVent = true;
            sit.ScaledDown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == vent)
        {
            ph.inVent = false;
            sit.ScaledUp();
        }
    }
}
