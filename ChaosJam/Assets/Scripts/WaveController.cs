using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    public static event Action<int> OnWave;
    public static event Action OnEnemy;
    public static event Action OnBoss;
    public static event Action<WaveController> OnDifficulty;


    [HideInInspector]
    public int waves;
    private float Timer;
    private float SpawnTimer;
    public float RoundLenght;
    public float spawnTime;
    public float spawnLenght;
    private float spawnCounter;
    public float spawnDelay;
    bool happenOnce = false;
    public Resourses resources;

    private void Update()
    {
        OnWave?.Invoke(waves);

        if (waves <= 9)
        {
            Timer += Time.deltaTime;
            SpawnTimer += Time.deltaTime;
            spawnCounter += Time.deltaTime;
        }

        if (spawnCounter > spawnDelay)
        {
            if (Timer >= RoundLenght)
            {
                Timer = 0;
                waves++;
                spawnCounter = 0;
                OnDifficulty?.Invoke(this);
                resources.resources = resources.resources + 75;
            }

            if (SpawnTimer >= spawnTime && Timer < spawnLenght && spawnCounter > spawnDelay)
            {
                SpawnTimer = 0;
                //SpawnEnemy();
                if(waves <= 8)
                    OnEnemy?.Invoke();
            }
        }
        else
        {
            Debug.Log("round cool down");
        }

        if(waves == 9)
        {
            if (!happenOnce)
            {
                Debug.Log("Boss Spawning");
                OnBoss?.Invoke();
                happenOnce = true;
            }
            //
            Timer = 0;
        }    


        if(Timer >= spawnLenght)
        {
            Debug.Log("No longer spawning");
        }
    }
}
