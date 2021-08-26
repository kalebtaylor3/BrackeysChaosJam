using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dificulty : MonoBehaviour
{
    private void OnEnable()
    {
        WaveController.OnDifficulty += ChangeDifficulty;
    }

    private void OnDisable()
    {
        WaveController.OnDifficulty -= ChangeDifficulty;
    }

    void ChangeDifficulty(WaveController stats)
    {
        float IncreaseMultuplier = 1.10f;
        float DecreaseMultiplier = 0.25f;

        stats.RoundLenght = stats.RoundLenght * IncreaseMultuplier;
        stats.spawnTime = stats.spawnTime * DecreaseMultiplier;
        stats.spawnLenght = stats.spawnLenght * IncreaseMultuplier;
        stats.spawnDelay = stats.spawnDelay * IncreaseMultuplier;

        Debug.Log(stats.spawnLenght);
    }
}
