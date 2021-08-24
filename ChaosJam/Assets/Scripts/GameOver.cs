using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    int playerCounter = 1;
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
        if(playerCounter == 0)
        {
            //StartCoroutine(DeathDelay());
        }
    }

    void Increase()
    {
        playerCounter = playerCounter + 1;
        Debug.Log(playerCounter);
    }

    void Decrease()
    {
        playerCounter = playerCounter - 1;
        Debug.Log(playerCounter);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Application.LoadLevel(2);
    }
}
