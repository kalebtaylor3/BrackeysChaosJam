using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSo> buildingTypeList;
    [SerializeField] private PlacementManager manager;
    public static event Action DisableBuilder;
    public static event Action DisableDelete;
    [HideInInspector] public Dictionary<BuildingTypeSo, Transform> buildingbtnDic;
    private void Awake()
    {
        Transform buildingButtonTemplate = transform.Find("BuildingButtonTemplate");
        buildingButtonTemplate.gameObject.SetActive(false);


        buildingbtnDic = new Dictionary<BuildingTypeSo, Transform>();

        int index = 0;
        foreach (BuildingTypeSo buildingTypeSo in buildingTypeList)
        {
            Transform buildingButtonTransform =  Instantiate(buildingButtonTemplate, transform);
            buildingButtonTransform.gameObject.SetActive(true);

            buildingButtonTransform.GetComponent<RectTransform>().anchoredPosition += new Vector2(index * 100, 0);
            buildingButtonTransform.Find("image").GetComponent<Image>().sprite = buildingTypeSo.image;

            buildingButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                manager.SetActiveType(buildingTypeSo);
                DisableBuilder?.Invoke();
                DisableDelete?.Invoke();
                UpdateSelectedVisual();
                manager.wallMode = true;
                manager.selectedRepair.SetActive(false);
                //makeInvisable?.Invoke();
            });
            buildingbtnDic[buildingTypeSo] = buildingButtonTransform;
            index++;
        }

    }

    private void Start()
    {
        UpdateSelectedVisual();
        foreach (BuildingTypeSo buildingTypeSo in buildingbtnDic.Keys)
        {
            buildingbtnDic[buildingTypeSo].Find("Selected").gameObject.SetActive(false);
        }
    }

    private void UpdateSelectedVisual()
    {
        foreach(BuildingTypeSo buildingTypeSo in buildingbtnDic.Keys)
        {
            buildingbtnDic[buildingTypeSo].Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSo activebuildingType = manager.GetActiveBuildingType();
        buildingbtnDic[activebuildingType].Find("Selected").gameObject.SetActive(true);
    }
}
