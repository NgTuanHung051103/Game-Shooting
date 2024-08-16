using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public bool isHasKey1 = false;
    public bool isHasKey2 = false;
    public static KeyController Instance { get; set; }
    private void Awake()
    {
        if(Instance != null && Instance != this )
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }     

    public void PickupKey(GameObject pickedupKey)
    {
        if(pickedupKey.tag == "Key1")
        {
            Destroy(pickedupKey);
            isHasKey1 = true;
        }
        else if(pickedupKey.tag == "Key2")
        {
            Destroy(pickedupKey);
            isHasKey2 = true;
        }
    }
}
