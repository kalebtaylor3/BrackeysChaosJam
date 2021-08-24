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
    private float spawnCounter;
    public float spawnDelay;

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
            }

            if (SpawnTimer >= spawnTime && Timer < spawnLenght && spawnCounter > spawnDelay)
            {
                SpawnTimer = 0;
                //SpawnEnemy();
                if(waves <= 9)
                    OnEnemy?.Invoke();

                if(waves == 10)
                {
                    //spawn boss
                }
            }
        }
        else
        {
            Debug.Log("round cool down");
        }

        if(waves == 10)
        {
            //
            Timer = 0;
            //if boss dies
                //waves = 11;
                //load gameover win
        }    


        if(Timer >= spawnLenght)
        {
            Debug.Log("No longer spawning");
        }
    }
}
