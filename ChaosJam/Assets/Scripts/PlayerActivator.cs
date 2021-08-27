using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerActivator : MonoBehaviour
{

    public GameObject[] Spawn;
    public GameObject Player;
    bool canSpawn = true;

    public GameObject[] NextPlayerIndicator;

    public static event Action OnSpawn;

    public WaveController waves;

    // Start is called before the first frame update
    private void OnEnable()
    {
        WaveController.OnWave += SpawnPlayer;
    }

    private void OnDisable()
    {
        WaveController.OnWave -= SpawnPlayer;
    }

    private void Start()
    {
        NextPlayerIndicator[0].SetActive(false);
        NextPlayerIndicator[1].SetActive(false);
        NextPlayerIndicator[2].SetActive(false);
        NextPlayerIndicator[3].SetActive(false);
    }

        void SpawnPlayer(int wave)
        {
            GameObject go;

            switch (wave)
            {
                case 1:
                    if (waves.Timer >= waves.RoundLenght - 5)
                    {
                        NextPlayerIndicator[0].SetActive(true);
                        NextPlayerIndicator[1].SetActive(false);
                        NextPlayerIndicator[2].SetActive(false);
                        NextPlayerIndicator[3].SetActive(false);
                    }
                    break;
                case 2:
                    NextPlayerIndicator[0].SetActive(false);
                    NextPlayerIndicator[1].SetActive(false);
                    NextPlayerIndicator[2].SetActive(false);
                    NextPlayerIndicator[3].SetActive(false);
                    if (canSpawn)
                    {
                        go = Instantiate(Player, Spawn[0].transform.position, Quaternion.identity);
                        go.transform.SetParent(Spawn[0].transform);
                        OnSpawn?.Invoke();
                        canSpawn = false;
                    }
                    break;
                case 3:
                    if (waves.Timer >= waves.RoundLenght - 5)
                    {
                        NextPlayerIndicator[0].SetActive(false);
                        NextPlayerIndicator[1].SetActive(true);
                        NextPlayerIndicator[2].SetActive(false);
                        NextPlayerIndicator[3].SetActive(false);
                    }
                    canSpawn = true;
                    break;
                case 4:
                    NextPlayerIndicator[0].SetActive(false);
                    NextPlayerIndicator[1].SetActive(false);
                    NextPlayerIndicator[2].SetActive(false);
                    NextPlayerIndicator[3].SetActive(false);
                    if (canSpawn)
                    {
                        go = Instantiate(Player, Spawn[1].transform.position, Quaternion.identity);
                        go.transform.SetParent(Spawn[1].transform);
                        OnSpawn?.Invoke();
                        canSpawn = false;
                    }
                    break;
                case 5:
                    if (waves.Timer >= waves.RoundLenght - 5)
                    {
                        NextPlayerIndicator[0].SetActive(false);
                        NextPlayerIndicator[1].SetActive(false);
                        NextPlayerIndicator[2].SetActive(true);
                        NextPlayerIndicator[3].SetActive(false);
                    }
                    canSpawn = true;
                    break;
                case 6:
                    NextPlayerIndicator[0].SetActive(false);
                    NextPlayerIndicator[1].SetActive(false);
                    NextPlayerIndicator[2].SetActive(false);
                    NextPlayerIndicator[3].SetActive(false);
                    if (canSpawn)
                    {
                        go = Instantiate(Player, Spawn[2].transform.position, Quaternion.identity);
                        go.transform.SetParent(Spawn[2].transform);
                        OnSpawn?.Invoke();
                        canSpawn = false;
                    }
                    break;
                case 7:
                    if (waves.Timer >= waves.RoundLenght - 5)
                    {
                        NextPlayerIndicator[0].SetActive(false);
                        NextPlayerIndicator[1].SetActive(false);
                        NextPlayerIndicator[2].SetActive(false);
                        NextPlayerIndicator[3].SetActive(true);
                    }
                    canSpawn = true;
                    break;
                case 8:
                    NextPlayerIndicator[0].SetActive(false);
                    NextPlayerIndicator[1].SetActive(false);
                    NextPlayerIndicator[2].SetActive(false);
                    NextPlayerIndicator[3].SetActive(false);
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
