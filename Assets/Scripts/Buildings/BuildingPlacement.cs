using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] GameObject buildingButtons;
    [SerializeField] private BuildingSpot[] buildingSpots;
    
    [HideInInspector] public GameObject chosenBuildingPrefab;
    private BuildingSpot prevBuildingSpot;

    private BuildingSlot buildingSlot;

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

                    prevBuildingSpot.ClearSpot(true);

                    buildingSpot.Place(chosenBuildingPrefab);
                    buildingSpot.SetBuildingSlot(buildingSlot);
                    prevBuildingSpot = buildingSpot;
                }
            }
        }
    }

    public void ChooseBuilding(GameObject building)
    {
        chosenBuildingPrefab = building;

        for (int i = 0; i < buildingSpots.Length; i++)
        {
            if (buildingSpots[i].IsBusy() == false)
            {
                buildingSpots[i].Place(chosenBuildingPrefab);
                buildingSpots[i].SetBuildingSlot(buildingSlot);
                prevBuildingSpot = buildingSpots[i];
                return;
            }
        }
        
        chosenBuildingPrefab = null;
        buildingButtons.SetActive(true);
    }

    public void SetBuildingSlot(BuildingSlot button)
    {
        buildingSlot = button;
    }
}
