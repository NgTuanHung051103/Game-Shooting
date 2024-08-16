using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowAI : MonoBehaviour
{
    Transform Player;
    float distance;
    public float howClose = 100f;
    public Weapon _weapon;
    public float _health = 2000f;
    public float fireRate, nextFire;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);
        if(distance <= howClose)
        {
            _weapon.transform.LookAt(Player);
            if( Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(_weapon.bulletPrefab, _weapon.bulletSpawn.position, _weapon.bulletSpawn.rotation);
        
        bullet.GetComponent<Rigidbody>().AddForce(_weapon.transform.forward * 1500);

        Destroy(bullet, 10);
    }
}