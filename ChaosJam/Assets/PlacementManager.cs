using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private BuildingTypeSo activeType;
    public static event Action<int> OnPlace;
    public static event Action makeVisable;
    public static event Action delMode;
    public static event Action makeInvisable;
    public static event Action DisabledelMode;
    public bool buildMode = false;
    public bool deleteMode = false;
    public bool wallMode = false;
    public Resourses resources;
    public BuildingTypeSelectUI ui;
    public GameObject _place;
    //public int wallCost = 24;

    public GameObject vertical;
    public GameObject Horitzotal;
    public GameObject repair;


    public static event Action OnParticle;

    public GameObject selectedRepair;
    public GameObject selectedDelete;
    public SpriteRenderer h;
    public SpriteRenderer vv;
    public SpriteRenderer r;


    public Color can;
    public Color cant;
    public Color cantR;

    float counter;

    private void Start()
    {
        selectedRepair.SetActive(false);
        selectedDelete.SetActive(false);
        vertical.SetActive(false);
        Horitzotal.SetActive(false);
        repair.SetActive(false);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        if (!canSpawn(activeType, mouseWorldPosition))
        {
            h.color = cant;
            vv.color = cant;
        }
        else if(canSpawn(activeType, mouseWorldPosition))
        {
            if (resources.resources < 25)
            {
                h.color = cant;
                vv.color = cant;
            }
            else
            {
                h.color = can;
                vv.color = can;
            }
        }

        if(resources.resources < 5)
        {
            r.color = cantR;
        }
        else
        {
            r.color = Color.black;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);
        foreach (Collider2D c in col)
        {
            //Debug.Log("Collided with: " + c.collider2D.gameObject.name);
            //targetPos = c.collider2D.gameObject.transform.position;

            if (c.gameObject.tag == "Wall")
            {
                if (buildMode)
                    makeVisable?.Invoke();

                if(deleteMode)
                {
                    delMode?.Invoke();
                }
            }
            else
            {
                makeInvisable?.Invoke();
                DisabledelMode?.Invoke();
            }

        }

        if (Input.GetMouseButtonDown(0) && buildMode == false && deleteMode == false && wallMode == true && !EventSystem.current.IsPointerOverGameObject())
        {
            //Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            if (canSpawn(activeType, mouseWorldPosition))
            {
                if (resources.resources >= 25)
                {
                    OnPlace?.Invoke(25);

                    OnParticle?.Invoke();
                    Transform active = Instantiate(activeType.prefab, mouseWorldPosition, Quaternion.identity);
                    //Instantiate(_place, mouseWorldPosition, active.rotation);
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
        repair.SetActive(true);
        buildMode = true;
        deleteMode = false;
        wallMode = false;
        selectedRepair.SetActive(true);
        selectedDelete.SetActive(false);
        vertical.SetActive(false);
        Horitzotal.SetActive(false);
        foreach (BuildingTypeSo buildingTypeSo in ui.buildingbtnDic.Keys)
        {
            ui.buildingbtnDic[buildingTypeSo].Find("Selected").gameObject.SetActive(false);
        }
    }

    public void DeleteMode()
    {
        repair.SetActive(false);
        deleteMode = true;
        wallMode = false;
        buildMode = false;
        selectedRepair.SetActive(false);
        selectedDelete.SetActive(true);
        vertical.SetActive(false);
        Horitzotal.SetActive(false);
        foreach (BuildingTypeSo buildingTypeSo in ui.buildingbtnDic.Keys)
        {
            ui.buildingbtnDic[buildingTypeSo].Find("Selected").gameObject.SetActive(false);
        }
    }

    public void SetActiveType(BuildingTypeSo building)
    {
        repair.SetActive(false);
        activeType = building;
        buildMode = false;
        deleteMode = false;
        selectedRepair.SetActive(false);
        selectedDelete.SetActive(false);

        if(activeType.prefab.name == "VerticalWallHolder")
        {
            vertical.SetActive(true);
            Horitzotal.SetActive(false);
        }
        else
        {
            Horitzotal.SetActive(true);
            vertical.SetActive(false);
        }
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
                Horitzotal.GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (c.transform.tag == "Player")
            {
                return false;
            }

            if (c.transform.tag == "Tree")
            {
                return false;
            }


        }

        return true;
        Horitzotal.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
