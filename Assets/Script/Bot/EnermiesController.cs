using UnityEngine;
using TMPro;
using System;
public class EnermiesController : MonoBehaviour
{
    public FollowAI Bot1;
    public static EnermiesController Instance { get; set; }
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
        // if( gun.CompareTag("AK47"))
        // {
        //     Bot1._health -= Constant.damage_AK47;
        // }
        // else if( gun.CompareTag("Marshal"))
        // {
        Bot1._health -= Constant.damage_AK47;
        // }
        healthRemaining.text = String.Format("Enermy: {0} hp", Bot1._health);
    
        if( Bot1._health == 0 )
        {
            Destroy(Bot1.gameObject);
            message.text = String.Format("You won!");
        }
    }
}