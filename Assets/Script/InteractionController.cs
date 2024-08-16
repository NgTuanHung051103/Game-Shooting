using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance { get; set; }
    public Weapon hoveredWeapon = null;
    public Key hoveredKey = null;
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

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRayCast = hit.transform.gameObject;

            if(objectHitByRayCast.GetComponent<Weapon>() && objectHitByRayCast.GetComponent<Weapon>().isHold == false)
            {
                hoveredWeapon = objectHitByRayCast.gameObject.GetComponent<Weapon>();
                
                // check exist of outline in a weapon
                if( hoveredWeapon.GetComponent<Outline>() == null)
                {
                    return;
                }

                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if(Input.GetKeyDown(KeyCode.F))
                {
                    WeaponController.Instance.PickupWeapon(objectHitByRayCast.gameObject);
                }
            }
            else if( objectHitByRayCast.GetComponent<Key>())
            {
                hoveredKey = objectHitByRayCast.gameObject.GetComponent<Key>();
                hoveredKey.GetComponent<Outline>().enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    KeyController.Instance.PickupKey(objectHitByRayCast.gameObject);
                }
            }
            else
            {   
                if(hoveredWeapon != null && hoveredWeapon.GetComponent<Outline>() != null)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                else if(hoveredKey != null)
                {
                    hoveredKey.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}