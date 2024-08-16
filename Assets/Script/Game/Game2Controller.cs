using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
public class Game2Controller : MonoBehaviour
{
     public static Game2Controller Instance { get; set; }
    public bool isDone = false;
    public List<GameObject> listObjects;
    private int countShootedTarget = 0;
    private bool isSpawnKey2 = false;
    public Vector3 spawnPositionKey2;
    public GameObject Key;
    public TextMeshProUGUI _score;

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
    public void ShootOnTarget()
    {
        countShootedTarget++;
        _score.text = String.Format("Score: {0}/{1}", countShootedTarget * 10, listObjects.Count * 10);
        SpawnKey2();
    }
    
    private void SpawnKey2()
    {
        if( !isSpawnKey2 && countShootedTarget == listObjects.Count )
        {
            Instantiate(Key, spawnPositionKey2, Quaternion.identity);
            isDone = true;
            isSpawnKey2 = true;
        }
    }

    public void StartGame()
    {
         
    }
}
