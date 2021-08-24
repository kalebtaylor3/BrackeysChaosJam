using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActivator : MonoBehaviour
{

    public GameObject[] Spawn;
    public GameObject Player;
    bool canSpawn = true;

    public static event Action OnSpawn;

    // Start is called before the first frame update
    private void OnEnable()
    {
        WaveController.OnWave += SpawnPlayer;
    }

    private void OnDisable()
    {
        WaveController.OnWave -= SpawnPlayer;
    }

    void SpawnPlayer(int wave)
    {
        GameObject go;

        switch (wave)
        {
            case 2:
                if (canSpawn)
                {
                    go = Instantiate(Player, Spawn[0].transform.position, Quaternion.identity);
                    go.transform.SetParent(Spawn[0].transform);
                    OnSpawn?.Invoke();
                    canSpawn = false;
                }
                break;
            case 3:
                canSpawn = true;
                break;
            case 4:
                if (canSpawn)
                {
                    go = Instantiate(Player, Spawn[1].transform.position, Quaternion.identity);
                    go.transform.SetParent(Spawn[1].transform);
                    OnSpawn?.Invoke();
                    canSpawn = false;
                }
                break;
            case 5:
                canSpawn = true;
                break;
            case 6:
                if (canSpawn)
                {
                    go = Instantiate(Player, Spawn[2].transform.position, Quaternion.identity);
                    go.transform.SetParent(Spawn[2].transform);
                    OnSpawn?.Invoke();
                    canSpawn = false;
                }
                break;
            case 7:
                canSpawn = true;
                break;
            case 8:
                if (canSpawn)
                {
                    go = Instantiate(Player, Spawn[3].transform.position, Quaternion.identity);
                    go.transform.SetParent(Spawn[3].transform);
                    OnSpawn?.Invoke();
                    canSpawn = false;
                }
                break;
        }
    }
}
