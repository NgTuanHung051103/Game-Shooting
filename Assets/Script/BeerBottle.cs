using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public GameObject Key;
    public Vector3 keySpawnOffset;

    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }

        if (Key != null)
        {
            Instantiate(Key, transform.position + keySpawnOffset, Quaternion.identity);
        }
    }
}
