using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Wall"))
        {
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Beer"))
        {
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
        }
        
        if(objectWeHit.gameObject.CompareTag("Enermy1"))
        {
            EnermiesController.Instance.TakeDamage();
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage();
            CreatBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
    }

    void CreatBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.GetContact(0);

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
