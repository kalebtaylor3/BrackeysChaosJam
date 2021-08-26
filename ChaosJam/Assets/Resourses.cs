using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Resourses : MonoBehaviour
{

    public int resources = 200;
    public Text text;
    bool hasHappened = false;

    [SerializeField] private GameObject floatingText;
    [SerializeField] private GameObject Dust;

    private void OnEnable()
    {
        //wall.UseResources += Repair;
        enemy.OnDeath += IncreaseZombieResouces;
        PlacementManager.OnPlace += UseResources;
        wall.UseResources += Repair;
        wall.GetResources += IncreaseResouces;
    }

    private void Start()
    {
        text.text = resources.ToString();
    }

    private void Update()
    {
        text.text = resources.ToString();
    }

    void Repair(int amount, HealthSystem repair, wall whichwall)
    {
        if (resources >= amount && repair.GetHealth() < 100)
        {
            resources = resources - amount;
            repair.RepairHealth();
            Debug.Log(resources);
            GameObject prefabDust = Instantiate(Dust, whichwall.transform.position, whichwall.transform.rotation);
            GameObject prefabText = Instantiate(floatingText, whichwall.transform.position, Quaternion.identity);
            prefabText.GetComponentInChildren<TextMesh>().text = "-" + amount.ToString();
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
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            GameObject prefabText = Instantiate(floatingText, mouseWorldPosition, Quaternion.identity);
            prefabText.GetComponentInChildren<TextMesh>().text = "-" + amount.ToString();
            resources = resources - amount;
        }
        else
        {
            Debug.Log("Not Enough Resources");
        }
    }

    void IncreaseResouces(int Amount)
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        GameObject prefabText = Instantiate(floatingText, mouseWorldPosition, Quaternion.identity);
        prefabText.GetComponentInChildren<TextMesh>().text = "+" + Amount.ToString();
        resources = resources + Amount;
    }

    void IncreaseZombieResouces(int Amount, enemy whichone)
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        GameObject prefabText = Instantiate(floatingText, whichone.transform.position, Quaternion.identity);
        prefabText.GetComponentInChildren<TextMesh>().text = "+" + Amount.ToString();
        resources = resources + Amount;
    }
}
