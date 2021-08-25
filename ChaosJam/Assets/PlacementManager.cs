using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private BuildingTypeSo activeType;
    public static event Action<int> OnPlace;
    public static event Action makeVisable;
    public static event Action makeInvisable;
    public bool buildMode = false;
    public bool wallMode = false;
    public Resourses resources;
    public BuildingTypeSelectUI ui;

    //public int wallCost = 24;

    public GameObject selectedRepair;

    float counter;

    private void Start()
    {
        selectedRepair.SetActive(false);
    }

    private void Update()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);

        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                //Debug.Log("Collided with: " + c.collider2D.gameObject.name);
                //targetPos = c.collider2D.gameObject.transform.position;

                if (c.gameObject.tag == "Wall")
                {
                    if (buildMode)
                        makeVisable?.Invoke();
                }
                else
                {
                    makeInvisable?.Invoke();
                }

            }

        }

        if (Input.GetMouseButtonDown(0) && buildMode == false && wallMode == true && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            if (canSpawn(activeType, mouseWorldPosition))
            {
                if (resources.resources >= 25)
                {
                    OnPlace?.Invoke(25);
                    Instantiate(activeType.prefab, mouseWorldPosition, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Not enough reesources");
                }
            }
            else
            {
                Debug.Log("Invalid Placement");
            }
        }

    }

    public void BuildMode()
    {
        buildMode = true;
        selectedRepair.SetActive(true);
        foreach (BuildingTypeSo buildingTypeSo in ui.buildingbtnDic.Keys)
        {
            ui.buildingbtnDic[buildingTypeSo].Find("Selected").gameObject.SetActive(false);
        }
    }

    public void SetActiveType(BuildingTypeSo building)
    {
        activeType = building;

        buildMode = false;
        selectedRepair.SetActive(false);
    }

    public BuildingTypeSo GetActiveBuildingType()
    {
        return activeType;
    }


    private bool canSpawn(BuildingTypeSo buildingSo, Vector3 Position)
    {

        Collider2D[] col = Physics2D.OverlapPointAll(Position);

        foreach (Collider2D c in col)
        {
            if(c.transform.tag == "Wall")
            {
                return false;
            }

            if (c.transform.tag == "Player")
            {
                return false;
            }


        }

        return true;
    }

}
