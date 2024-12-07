using UnityEngine;

public class BuildingChoose : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
