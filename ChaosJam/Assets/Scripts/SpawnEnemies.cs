using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject Boss;

    public GameObject[] LeftSpawns;
    public GameObject[] RightSpawns;

    private void OnEnable()
    {
        WaveController.OnEnemy += SpawnEnemy;
        WaveController.OnBoss += SpawnBoss;
    }
    private void OnDisable()
    {
        WaveController.OnEnemy -= SpawnEnemy;
        WaveController.OnBoss -= SpawnBoss;
    }

    void SpawnEnemy()
    {
        Debug.Log("EnemySpawned");

        int r = UnityEngine.Random.Range(1, 4);
        GameObject go;
        switch (r)
        {
            case 1:

                go = Instantiate(Enemy) as GameObject;
                go.transform.SetParent(transform);

                float spawnPointYLeft = UnityEngine.Random.Range(LeftSpawns[0].transform.position.y, LeftSpawns[1].transform.position.y);
                float spawnPointXLeft = UnityEngine.Random.Range(LeftSpawns[1].transform.position.x, LeftSpawns[0].transform.position.x);
                Vector3 spawnLeft = new Vector3(spawnPointXLeft, spawnPointYLeft, 0);
                go.transform.position = spawnLeft;
                break;
            case 2:

                go = Instantiate(Enemy) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYRight = UnityEngine.Random.Range(RightSpawns[0].transform.position.y, RightSpawns[1].transform.position.y);
                float spawnPointXRight = UnityEngine.Random.Range(RightSpawns[1].transform.position.x, RightSpawns[0].transform.position.x);
                Vector3 spawnRight = new Vector3(spawnPointXRight, spawnPointYRight, 0);
                go.transform.position = spawnRight;
                break;

            case 3:

                go = Instantiate(Enemy) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYTop = UnityEngine.Random.Range(RightSpawns[1].transform.position.y, LeftSpawns[1].transform.position.y);
                float spawnPointXTop = UnityEngine.Random.Range(RightSpawns[1].transform.position.x, LeftSpawns[1].transform.position.x);
                Vector3 spawnTop = new Vector3(spawnPointXTop, spawnPointYTop, 0);
                go.transform.position = spawnTop;
                break;

            case 4:

                go = Instantiate(Enemy) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYBottom = UnityEngine.Random.Range(RightSpawns[0].transform.position.y, LeftSpawns[0].transform.position.y);
                float spawnPointXBottom = UnityEngine.Random.Range(RightSpawns[0].transform.position.x, LeftSpawns[0].transform.position.x);
                Vector3 spawnBottom = new Vector3(spawnPointXBottom, spawnPointYBottom, 0);
                go.transform.position = spawnBottom;
                break;
        }
    }

    void SpawnBoss()
    {
        int r = UnityEngine.Random.Range(1, 4);
        GameObject go;
        switch (r)
        {
            case 1:

                go = Instantiate(Boss) as GameObject;
                go.transform.SetParent(transform);

                float spawnPointYLeft = UnityEngine.Random.Range(LeftSpawns[0].transform.position.y, LeftSpawns[1].transform.position.y);
                float spawnPointXLeft = UnityEngine.Random.Range(LeftSpawns[1].transform.position.x, LeftSpawns[0].transform.position.x);
                Vector3 spawnLeft = new Vector3(spawnPointXLeft, spawnPointYLeft, 0);
                go.transform.position = spawnLeft;
                break;
            case 2:

                go = Instantiate(Boss) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYRight = UnityEngine.Random.Range(RightSpawns[0].transform.position.y, RightSpawns[1].transform.position.y);
                float spawnPointXRight = UnityEngine.Random.Range(RightSpawns[1].transform.position.x, RightSpawns[0].transform.position.x);
                Vector3 spawnRight = new Vector3(spawnPointXRight, spawnPointYRight, 0);
                go.transform.position = spawnRight;
                break;

            case 3:

                go = Instantiate(Boss) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYTop = UnityEngine.Random.Range(RightSpawns[1].transform.position.y, LeftSpawns[1].transform.position.y);
                float spawnPointXTop = UnityEngine.Random.Range(RightSpawns[1].transform.position.x, LeftSpawns[1].transform.position.x);
                Vector3 spawnTop = new Vector3(spawnPointXTop, spawnPointYTop, 0);
                go.transform.position = spawnTop;
                break;

            case 4:

                go = Instantiate(Boss) as GameObject;
                go.transform.SetParent(transform);
                float spawnPointYBottom = UnityEngine.Random.Range(RightSpawns[0].transform.position.y, LeftSpawns[0].transform.position.y);
                float spawnPointXBottom = UnityEngine.Random.Range(RightSpawns[0].transform.position.x, LeftSpawns[0].transform.position.x);
                Vector3 spawnBottom = new Vector3(spawnPointXBottom, spawnPointYBottom, 0);
                go.transform.position = spawnBottom;
                break;
        }
    }
}
