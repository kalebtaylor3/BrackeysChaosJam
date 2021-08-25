using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private BuildingTypeSo activeType;
    public static event Action<int> OnPlace; 
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //ifresources is gater than amount
                OnPlace?.Invoke(25);
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                Instantiate(activeType.prefab, mouseWorldPosition, Quaternion.identity);
        }
    }

    public void SetActiveType(BuildingTypeSo building)
    {
        activeType = building;
    }

}
