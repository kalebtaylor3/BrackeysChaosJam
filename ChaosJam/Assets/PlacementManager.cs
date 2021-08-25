using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private BuildingTypeSo activeType;
    public static event Action<int> OnPlace;
    public static event Action makeVisable;
    public static event Action makeInvisable;
    public bool buildMode = false;
    Ray ray;
    RaycastHit hit;

    float counter;
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

        if (Input.GetMouseButtonDown(0) && buildMode == false)
        {
            //ifresources is gater than amount
                OnPlace?.Invoke(25);
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                Instantiate(activeType.prefab, mouseWorldPosition, Quaternion.identity);
        }

    }

    public void BuildMode()
    {
        buildMode = true;
    }

    public void SetActiveType(BuildingTypeSo building)
    {
        activeType = building;

        buildMode = false;
    }

}
