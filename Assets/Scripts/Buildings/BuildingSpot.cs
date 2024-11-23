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

        if (PlayerPrefs.HasKey(gameObject.name + "Building " + loadedSlotName))
        {
            foreach (GameObject building in buildingPrefabs)
            {
                if (building.name + "(Clone)" == PlayerPrefs.GetString(gameObject.name + "Building " + loadedSlotName))
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

        if (!PlayerPrefs.HasKey(gameObject.name + "Building " + loadedSlotName))
            PlayerPrefs.SetString(gameObject.name + "Building " + loadedSlotName, currentBuilding.name);
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

        PlayerPrefs.DeleteKey(gameObject.name + "Building " + loadedSlotName);
    }

    public bool IsBusy()
    {
        return isBusy;
    }
}
