using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Resourses : MonoBehaviour
{

    public int resources = 200;
    public Text text;

    private void OnEnable()
    {
        //wall.UseResources += Repair;
        enemy.OnDeath += IncreaseResouces;
        PlacementManager.OnPlace += UseResources;
        wall.UseResources += Repair;
    }

    private void Start()
    {
        text.text = resources.ToString();
    }

    private void Update()
    {
        text.text = resources.ToString();
    }

    void Repair(int amount, HealthSystem repair)
    {
        if (resources >= amount && repair.GetHealth() < 100)
        {
            resources = resources - amount;
            repair.RepairHealth();
            Debug.Log(resources);
        }
        else
        {
            Debug.Log("Not Enough Resources");
        }
    }

    void UseResources(int amount)
    {
        if (resources >= amount)
        {
            resources = resources - amount;
        }
        else
        {
            Debug.Log("Not Enough Resources");
        }
    }

    void IncreaseResouces(int Amount)
    {
        resources = resources + Amount;
    }
}
