using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [SerializeField] BuildingPlacement buildingPlacement;
    [SerializeField] GameObject buildingButtons;

    [SerializeField] private GameObject[] buildingPrefabs;

    private GameObject currentBuilding;
    private bool isBusy;

    private string loadedSlotName;

    private void Start()
    {
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

        if (!PlayerPrefs.HasKey(loadedSlotName + " " + gameObject.name + "Building"))
            PlayerPrefs.SetString(loadedSlotName + " " + gameObject.name + "Building", currentBuilding.name);
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
