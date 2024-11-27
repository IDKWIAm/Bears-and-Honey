using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private BuildingPlacement buildingPlacement;
    [SerializeField] private GameObject buildingButtons;
    [SerializeField] private DataPersistenceManager persistenceManager;

    [SerializeField] private GameObject[] buildingPrefabs;

    private GameObject currentBuilding;
    private bool isBusy;

    private string loadedSlotName;

    private void Start()
    {
        /*  TODO - make this part of code works without errors
        if (PlayerPrefs.HasKey("Loaded slot number"))
            persistenceManager.LoadGame(PlayerPrefs.GetInt("Loaded slot number"));
        */

        if (PlayerPrefs.HasKey("Loaded Save"))
        {
            loadedSlotName = PlayerPrefs.GetString("Loaded Save");
        }
        else return;

        if (PlayerPrefs.HasKey(loadedSlotName + " " + gameObject.name + "Building"))
        {
            foreach (GameObject building in buildingPrefabs)
            {
                if (building.name + "(Clone)" == PlayerPrefs.GetString(loadedSlotName + " " + gameObject.name + "Building"))
                {
                    Place(building);
                }
            }
        }
        
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, string> spotData in data.resources.spotsData.spotData)
        {
            if (spotData.Value == gameObject.name)
            {
                foreach (GameObject building in buildingPrefabs)
                {
                    if (building.name + "(Clone)" == spotData.Key)
                    {
                        Place(building);
                    }
                }
            }
        }
    }


    public void SaveData(ref GameData data)
    {
        if (currentBuilding == null) return;

        foreach (GameObject building in buildingPrefabs)
        {
            if (building == currentBuilding)
            {
                data.resources.spotsData.spotData.Add(currentBuilding.name, building.name);
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
        buildingPlacement.chosenBuildingPrefab = null;
        buildingButtons.SetActive(true);

        if (PlayerPrefs.HasKey("Loaded slot number"))
            persistenceManager.SaveGame(PlayerPrefs.GetInt("Loaded slot number"));
        else Debug.Log("Loaded slot number not found");
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
            buildingButtons.SetActive(true);
            buildingPlacement.chosenBuildingPrefab = null;
        }

        PlayerPrefs.DeleteKey(loadedSlotName + " " + gameObject.name + "Building");
    }

    public bool IsBusy()
    {
        return isBusy;
    }
}
