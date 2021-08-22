using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActivator : MonoBehaviour
{

    public GameObject[] Players;

    private void Start()
    {
        Players[0].SetActive(false);
        Players[1].SetActive(false);
        Players[2].SetActive(false);
        Players[3].SetActive(false);
        Players[4].SetActive(false);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        WaveController.OnWave += SpawnPlayer;
    }

    void SpawnPlayer(int wave)
    {
        switch (wave)
        {
            case 0:
                Players[0].SetActive(true);
                break;
            case 2:
                Players[1].SetActive(true);
                break;
            case 5:
                Players[2].SetActive(true);
                break;
            case 7:
                Players[3].SetActive(true);
                break;
            case 9:
                Players[4].SetActive(true);
                break;
        }
    }
}
