using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMover : MonoBehaviour
{

    public GameObject[] spawns;

    private void OnEnable()
    {
        WaveController.OnWave += CheckWave;
    }
    private void OnDisable()
    {
        WaveController.OnWave -= CheckWave;
    }

    void CheckWave(int wave)
    {
        switch (wave)
        {
            case 1:
                spawns[0].transform.position = new Vector3(-9.3f, 8.8f);
                spawns[1].transform.position = new Vector3(-9.3f, -8.8f);
                spawns[2].transform.position = new Vector3(9.4f, 8.7f);
                spawns[3].transform.position = new Vector3(9.4f, -8.7f);
                break;
            case 2:
                spawns[0].transform.position = new Vector3(-12.4f, 10.6f);
                spawns[1].transform.position = new Vector3(-12.4f, -10.6f);
                spawns[2].transform.position = new Vector3(12.7f, 11.2f);
                spawns[3].transform.position = new Vector3(12.7f, -11.2f);
                break;
            case 3:
                spawns[0].transform.position = new Vector3(-16.1f, 15f);
                spawns[1].transform.position = new Vector3(-16.1f, -15f);
                spawns[2].transform.position = new Vector3(16.3f, 13.8f);
                spawns[3].transform.position = new Vector3(16.3f, -13.8f);
                break;
            case 4:
                spawns[0].transform.position = new Vector3(-17.7f, 16.2f);
                spawns[1].transform.position = new Vector3(-17.7f, -16.2f);
                spawns[2].transform.position = new Vector3(18.1f, 16f);
                spawns[3].transform.position = new Vector3(18.1f, -16f);
                break;
            case 5:
                spawns[0].transform.position = new Vector3(-19.8f, 18f);
                spawns[1].transform.position = new Vector3(-19.8f, -18f);
                spawns[2].transform.position = new Vector3(19.2f, 17.7f);
                spawns[3].transform.position = new Vector3(19.2f, -17.7f);
                break;
            case 6:
                spawns[0].transform.position = new Vector3(-21.2f, 19.3f);
                spawns[1].transform.position = new Vector3(-21.2f, -19.3f);
                spawns[2].transform.position = new Vector3(21.2f, 18.9f);
                spawns[3].transform.position = new Vector3(21.2f, -18.9f);
                break;
            case 7:
                spawns[0].transform.position = new Vector3(-22.9f, 20f);
                spawns[1].transform.position = new Vector3(-22.9f, -20f);
                spawns[2].transform.position = new Vector3(22.9f, 20f);
                spawns[3].transform.position = new Vector3(22.9f, -20f);
                break;
            case 8:
                spawns[0].transform.position = new Vector3(-24.9f, 21.8f);
                spawns[1].transform.position = new Vector3(-24.9f, -21.8f);
                spawns[2].transform.position = new Vector3(24.9f, 21.8f);
                spawns[3].transform.position = new Vector3(24.9f, -21.8f);
                break;
        }
    }
}
