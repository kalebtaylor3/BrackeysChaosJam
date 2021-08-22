using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private void OnEnable()
    {
        RoundZoomer.OnWave += IncreaseWave;
    }

    void IncreaseWave(RoundZoomer wave)
    {
        wave.waves++;
    }
}
