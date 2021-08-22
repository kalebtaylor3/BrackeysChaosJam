using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    public static event Action<int> OnWave;
    public int waves;
    private float Timer;
    public float RoundLenght;

    private void Update()
    {
        OnWave?.Invoke(waves);
        Timer += Time.deltaTime;

        if (Timer >= RoundLenght)
        {
            Timer = 0;
            waves++;
        }
    }
}
