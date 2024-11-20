using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    private bool isBusy;

    private GameObject currentBuilding;

    public void Build(GameObject building)
    {
        currentBuilding = Instantiate(building, transform.position, quaternion.identity);
        currentBuilding.GetComponent<Building>()?.SetSpot(gameObject);
        isBusy = true;
    }

    public void ClearSpot()
    {
        if (currentBuilding != null)
        {
            Destroy(currentBuilding);
            isBusy = false;
        }
    }

    public bool IsBusy()
    {
        return isBusy;
    }
}
