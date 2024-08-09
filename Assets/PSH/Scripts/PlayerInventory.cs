using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;
    public bool hasPaper = false;
    public bool hasDriver = false;
    public bool hasNipper = false;

    public void SetKey(bool value)
    {
        hasKey = value;
        Debug.Log("Player has key: " + hasKey);
    }

    public void SetPaper(bool value)
    {
        hasPaper = value;
        Debug.Log("Player has paper: " + hasPaper);
    }

    public void SetDriver(bool value)
    {
        hasDriver = value;
        Debug.Log("Player has driver: " + hasDriver);
    }

    public void SetNipper(bool value)
    {
        hasNipper = value;
        Debug.Log("Player has nipper: " + hasNipper);
    }
}