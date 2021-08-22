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
        float multiplier = 1.25f;
        float multiplier2 = 0.75f;

        stats.RoundLenght = stats.RoundLenght * multiplier;
        stats.spawnTime = stats.spawnTime * multiplier2;
        stats.spawnLenght = stats.spawnLenght * multiplier;

        Debug.Log(stats.spawnLenght);
    }
}
