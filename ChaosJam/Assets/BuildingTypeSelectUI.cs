using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSo> buildingTypeList;
    [SerializeField] private PlacementManager manager;

    private void Awake()
    {
        Transform buildingButtonTemplate = transform.Find("BuildingButtonTemplate");
        buildingButtonTemplate.gameObject.SetActive(false);

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
            });

            index++;
        }
    }
}
