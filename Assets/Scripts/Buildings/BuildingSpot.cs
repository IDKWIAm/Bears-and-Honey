using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private BuildingPlacement buildingPlacement;
    [SerializeField] private GameObject buildingsMenu;

    [SerializeField] private WebRequestManager requestManager;
    [SerializeField] private DataPersistenceManager persistenceManager;

    [SerializeField] private GameObject[] buildingPrefabs;

    private GameObject buyButton;
    private GameObject currentBuilding;
    private bool isBusy;

    private bool loading;

    public void LoadData(GameData data)
    {
        loading = true;

        if (data.resources.spotsData != null)
        {
            foreach (KeyValuePair<string, string> spotData in data.resources.spotsData)
            {
                if (spotData.Key == gameObject.name)
                {
                    foreach (GameObject building in buildingPrefabs)
                    {
                        if (building.name == spotData.Value)
                        {
                            Place(building);
                            currentBuilding.GetComponent<Building>().ConfirmBuild();
                        }
                    }
                }
            }
        }
        loading = false;
    }


    public void SaveData(ref GameData data)
    {
        if (currentBuilding == null) return;

        foreach (GameObject building in buildingPrefabs)
        {
            if (building.name + "(Clone)" == currentBuilding.name)
            {
                if (data.resources.spotsData == null) data.resources.spotsData = new Dictionary<string, string>() { };
                if (!data.resources.spotsData.ContainsKey(gameObject.name))
                    data.resources.spotsData.Add(gameObject.name, building.name);
            }
        }
    }

    public void Place(GameObject building)
    {
        currentBuilding = Instantiate(building, transform.position, quaternion.identity);
        currentBuilding.GetComponent<Building>()?.SetSpot(gameObject);
        isBusy = true;
    }

    public void Build()
    {
        if (!loading)
        {
            if (PlayerPrefs.HasKey("Loaded slot number"))
            {
                persistenceManager.SaveGame(PlayerPrefs.GetString("Loaded slot name"), PlayerPrefs.GetInt("Loaded slot number"));
                StartCoroutine(requestManager.SendLog("Built new structure on " + gameObject.name, Environment.MachineName + " " + PlayerPrefs.GetString("Loaded slot name"),
                    new Dictionary<string, string>() { { "Structure added", currentBuilding.name.Substring(0, currentBuilding.name.Length - 7) } }));
            }
            else Debug.Log("Loaded slot number not found. Save is skipped.");
        }

        buildingPlacement.chosenBuildingPrefab = null;
        buildingsMenu.SetActive(true);
        buyButton?.SetActive(false);
    }

    public void ClearSpot(bool isMoving = false)
    {
        if (currentBuilding != null)
        {
            Destroy(currentBuilding);
            isBusy = false;
        }
        if (!isMoving)
        {
            buildingsMenu.SetActive(true);
            buildingPlacement.chosenBuildingPrefab = null;
        }
    }

    public bool IsBusy()
    {
        return isBusy;
    }

    public void SetBuyButton(GameObject button)
    {
        buyButton = button;
    }
}
