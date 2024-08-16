using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }
    public PlayerMovement _player; 
    public TextMeshProUGUI healthRemaining;
    public TextMeshProUGUI message;
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
    public void TakeDamage()
    {
        _player.HealthOfPlayer -= Constant.damage_AK47;
        healthRemaining.text = String.Format("Remain: {0} hp", _player.HealthOfPlayer);
        if( _player.HealthOfPlayer == 0 )
        {
            Destroy(_player.gameObject);
            message.text = String.Format("You losed!");
        }
    }
}