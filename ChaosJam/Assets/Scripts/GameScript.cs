﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        Debug.Log("Health: " +healthSystem.GetHealth());
        healthSystem.Damage(10);
        Debug.Log("Health: " + healthSystem.GetHealth());
        healthSystem.Damage(20);
        Debug.Log("Health: " + healthSystem.GetHealth()); 



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
