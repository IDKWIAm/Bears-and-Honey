using Unity.Mathematics;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    private bool isBusy;

    public void Build(GameObject building)
    {
        if (!isBusy)
        {
            Instantiate(building, transform.position, quaternion.identity);
            isBusy = true;
        }
    }
}
