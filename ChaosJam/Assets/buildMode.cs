using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class buildMode : MonoBehaviour
{

    public bool repairMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Awake()
    {
        Transform buildingButtonTemplate = transform.Find("RepairButton");
        Transform buildingButtonTransform = buildingButtonTemplate;

        buildingButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("repair mode");
        });
    }
}
