using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    int playerCounter = 1;
    int activePlayers = 1;
    bool isDone = false;
    public AudioSource bossDeath;
    private void OnEnable()
    {
        PlayerActivator.OnSpawn += Increase;
        Player.OnDeath += Decrease;
        Boss.OnDeath += winScreen;
    }

    private void OnDisable()
    {
        PlayerActivator.OnSpawn -= Increase;
        Player.OnDeath -= Decrease;

        Boss.OnDeath -= winScreen;
    }

    private void Update()
    {
        if(activePlayers == 0)
        {
            StartCoroutine(DeathDelay());
        }
    }

    void Increase()
    {
        playerCounter = playerCounter + 1;
        activePlayers = playerCounter;
        Debug.Log(playerCounter);
    }

    void Decrease()
    {
        playerCounter = playerCounter - 1;
        activePlayers = playerCounter;
        Debug.Log(playerCounter);
        isDone = true;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Application.LoadLevel(2);
    }

    void winScreen()
    {
        bossDeath.Play();
        StartCoroutine(winDelay());
    }

    IEnumerator winDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Application.LoadLevel(3);
    }
}
