using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; set; }
    public AudioSource ShootingChannel;
    public AudioClip AK47Shot;
    public AudioClip MarsalShot;
    public AudioSource reloadingSoundAK47;
    public AudioSource emptySoundAK47;
    public AudioSource reloadingSoundMarshal;

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
    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        switch(weapon)
        {
            case Weapon.WeaponModel.AK47:
                ShootingChannel.PlayOneShot(AK47Shot);
                break;
            case Weapon.WeaponModel.Marshal:
                ShootingChannel.PlayOneShot(MarsalShot);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch(weapon)
        {
            case Weapon.WeaponModel.AK47:
                reloadingSoundAK47.Play();
                break;
            case Weapon.WeaponModel.Marshal:
                reloadingSoundMarshal.Play();
                break;
        }
    }
}
