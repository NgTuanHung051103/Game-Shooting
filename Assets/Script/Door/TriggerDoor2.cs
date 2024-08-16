using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor2 : MonoBehaviour
{
    private Animator _doorAnimator;
    public bool isOpen = false;
    void Start()
    {
        _doorAnimator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            print(KeyController.Instance.isHasKey2);
            if( KeyController.Instance.isHasKey2 && !isOpen )
            {
                _doorAnimator.SetTrigger("Open");
                isOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isOpen)
            {
                print("Is closing");
                _doorAnimator.SetTrigger("Closed");
                isOpen = false;
            }
        }
    }
}