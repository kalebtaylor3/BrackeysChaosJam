using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public static event Action<int> OnWave;
    public static event Action OnEnemy;
    public static event Action OnBoss;
    public static event Action<WaveController> OnDifficulty;


    [HideInInspector]
    public int waves;
    public float Timer;
    public float SpawnTimer;
    public float RoundLenght;
    public float spawnTime;
    public float spawnLenght;
    private float spawnCounter;
    public float spawnDelay;
    bool happenOnce = false;
    public Resourses resources;

    public GameObject nextWave;
    public int secondsLeft = 5;
    public bool takingAway = false;
    public AudioSource newRound;
    public GameObject waveText;

    private void Start()
    {
        int currentWave = waves + 1;
        nextWave.GetComponent<Text>().text = "Next wave starting in 00:0" + 5;
        waveText.GetComponent<Text>().text = "Wave " + currentWave + "/10";
    }

    private void Update()
    {

        int currentWave = waves + 1;
        waveText.GetComponent<Text>().text = "Wave " + currentWave + "/10";

        if (Timer >= RoundLenght - 6)
        {
            nextWave.SetActive(true);
            if (takingAway == false && secondsLeft > 0)
            {
                nextWave.GetComponent<Text>().text = "Next wave starting in 00:0" + secondsLeft;
                StartCoroutine(TimerTake());
            }
        }
        else
        {
            nextWave.GetComponent<Text>().text = "";
        }

        OnWave?.Invoke(waves);

        if (waves <= 9)
        {
            Timer += Time.deltaTime;
            SpawnTimer += Time.deltaTime;
            spawnCounter += Time.deltaTime;
        }

        if (spawnCounter > spawnDelay)
        {
            if (Timer >= RoundLenght)
            {
                Timer = 0;
                waves++;
                newRound.Play();
                spawnCounter = 0;
                nextWave.SetActive(false);
                nextWave.GetComponent<Text>().text = "";
                secondsLeft = 5;
                takingAway = false;
                OnDifficulty?.Invoke(this);
                resources.resources = resources.resources + 75;
            }

            if (SpawnTimer >= spawnTime && Timer < spawnLenght && spawnCounter > spawnDelay)
            {
                SpawnTimer = 0;
                //SpawnEnemy();
                if(waves <= 8)
                    OnEnemy?.Invoke();
            }
        }
        else
        {
            Debug.Log("round cool down");
        }

        if(waves == 9)
        {
            if (!happenOnce)
            {
                Debug.Log("Boss Spawning");
                OnBoss?.Invoke();
                happenOnce = true;
            }
            //
            Timer = 0;
        }    


        if(Timer >= spawnLenght)
        {
            Debug.Log("No longer spawning");
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        nextWave.GetComponent<Text>().text = "Next wave starting in 00:0" + secondsLeft;
        takingAway = false;
    }
}
