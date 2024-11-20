using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    private GameObject chosenBuildingPrefab;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && chosenBuildingPrefab != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<BuildingSpot>(out BuildingSpot buildingSpot))
                {
                    if (buildingSpot.IsBusy()) return;

                    buildingSpot.Build(chosenBuildingPrefab);
                    chosenBuildingPrefab = null;
                }
            }
        }
    }

    public void ChooseBuilding(GameObject building)
    {
        chosenBuildingPrefab = building;
    }
}
