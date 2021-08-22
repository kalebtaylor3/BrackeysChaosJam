using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    public static event Action<int> OnWave;
    public static event Action OnEnemy;
    public static event Action<WaveController> OnDifficulty;


    [HideInInspector]
    public int waves;
    private float Timer;
    private float SpawnTimer;
    public float RoundLenght;
    public float spawnTime;
    public float spawnLenght;

    private void Update()
    {
        OnWave?.Invoke(waves);
        Timer += Time.deltaTime;
        SpawnTimer += Time.deltaTime;

        if (Timer >= RoundLenght)
        {
            Timer = 0;
            waves++;
            OnDifficulty?.Invoke(this);
        }

        if(SpawnTimer >= spawnTime && Timer < spawnLenght)
        {
            SpawnTimer = 0;
            //SpawnEnemy();
            OnEnemy?.Invoke();
        }

        if(Timer >= spawnLenght)
        {
            Debug.Log("No longer spawning");
        }
    }
}
