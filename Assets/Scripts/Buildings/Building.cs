using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject confirmButtons;

    private BuildingSpot spot;
    public void SetSpot(GameObject gameObject)
    {
        spot = gameObject.GetComponent<BuildingSpot>();
    }

    public void ConfirmBuild()
    {
        spot.Build();
        confirmButtons.SetActive(false);
    }

    public void SelfDestoy()
    {
        spot.ClearSpot();
    }
}
