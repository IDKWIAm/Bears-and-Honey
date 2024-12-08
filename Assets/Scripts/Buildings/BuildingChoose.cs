using UnityEngine;

public class BuildingChoose : MonoBehaviour
{
    [SerializeField] DraggableCamera draggableCamera;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (draggableCamera.isMovingAllowed == false) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<Building>(out Building building))
                {
                    building.activateMinigameButton();
                }
            }
        }
    }
}
