using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    int playerCounter = 1;
    int activePlayers = 1;
    bool isDone = false;
    private void OnEnable()
    {
        PlayerActivator.OnSpawn += Increase;
        Player.OnDeath += Decrease;
    }

    private void OnDisable()
    {
        PlayerActivator.OnSpawn -= Increase;
        Player.OnDeath -= Decrease;
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
}
