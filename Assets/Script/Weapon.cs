using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isHold;
    public Camera playerCamera;
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;
    
    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    
    //Spread
    public float _spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;
    // Start is called before the first frame update

    public enum ShootingMode
    {
        Single, 
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;
    
    public GameObject muzzleEffect;
    internal Animator animator;
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public enum WeaponModel
    {
        AK47,
        Marshal,
        Gun_Bot
    }
      
    public WeaponModel thisWeaponModel;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;
    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isHold)
        {
            if (bulletsLeft == 0 && isShooting)
            {
                SoundController.Instance.emptySoundAK47.Play();
            }

            if( currentShootingMode == ShootingMode.Auto)
            {
                // Holding down Left Mouse Button
                isShooting = Input.GetKey(KeyCode.Mouse0);
            } 
            else if ( currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst )
            {
                // Click Left Mouse Button
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }

            if( (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading ) 
                || (readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0 ) )
            {
                Reload();
            }

            if ( readyToShoot && isShooting )
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
            
            if(AmmoController.Instance.ammoDisplay != null)
            {
                AmmoController.Instance.ammoDisplay.text = $"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
            }
        }
       
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        
        animator.SetTrigger("RECOIL");

        SoundController.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        
        Vector3 shootingDirection = CalculateDirectionAndSpread(bulletSpawn.position, _spreadIntensity).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection* bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if( allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst Mode
        if ( currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // we already shoot once before this reset
        {
            burstBulletsLeft--;
        }
    }

    public IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    
    public Vector3 CalculateDirectionAndSpread(Vector3 positionSpawn, float spreadIntensity)
    {
        // Shooting from the middle of the screen to check where are we pointing at
        Ray ray = Camera.main.ViewportPointToRay( new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(100);
        }
        
        Vector3 direction = targetPoint - positionSpawn;

        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        // Returning the shooting direction and spread
        return direction + new Vector3(x, y, 0);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
    private void Reload()
    {
        SoundController.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("RELOAD");
        isReloading = true;
        readyToShoot = false;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
        readyToShoot = true;
    }
}