using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingSpot spot;
    public void SetSpot(GameObject gameObject)
    {
        spot = gameObject.GetComponent<BuildingSpot>();
    }

    public void ConfirmBuild()
    {
        spot.Build();
    }

    public void SelfDestoy()
    {
        spot.ClearSpot();
    }
}
