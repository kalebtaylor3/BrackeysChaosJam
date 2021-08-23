using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActivator : MonoBehaviour
{

    public GameObject[] Spawn;
    public GameObject Player;
    bool canSpawn = true;

    // Start is called before the first frame update
    private void OnEnable()
    {
        WaveController.OnWave += SpawnPlayer;
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
                    canSpawn = false;
                }
                break;
            case 4:
                go = Instantiate(Player, Spawn[1].transform.position, Quaternion.identity);
                go.transform.SetParent(Spawn[1].transform);
                break;
            case 6:
                go = Instantiate(Player, Spawn[2].transform.position, Quaternion.identity);
                go.transform.SetParent(Spawn[2].transform);
                break;
            case 8:
                go = Instantiate(Player, Spawn[3].transform.position, Quaternion.identity);
                go.transform.SetParent(Spawn[3].transform);
                break;
        }
    }
}
