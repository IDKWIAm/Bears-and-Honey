using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    [SerializeField] BuildingPlacement buildingPlacement;
    [SerializeField] GameObject buildingButtons;

    private GameObject currentBuilding;
    private bool isBusy;

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
    }

    public bool IsBusy()
    {
        return isBusy;
    }
}
