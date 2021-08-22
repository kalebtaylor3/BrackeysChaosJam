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
        float IncreaseMultuplier = 1.25f;
        float DecreaseMultiplier = 0.75f;

        stats.RoundLenght = stats.RoundLenght * IncreaseMultuplier;
        stats.spawnTime = stats.spawnTime * DecreaseMultiplier;
        stats.spawnLenght = stats.spawnLenght * IncreaseMultuplier;

        Debug.Log(stats.spawnLenght);
    }
}
