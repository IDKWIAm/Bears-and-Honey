using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject confirmButtons;

    private BuildingSpot spot;
    public void SetSpot(GameObject gameObject)
    {
        spot = gameObject.GetComponent<BuildingSpot>();

        if (PlayerPrefs.HasKey(spot.gameObject.name + "Building " + PlayerPrefs.GetString("Loaded Save")))
            ConfirmBuild();
    }

    public void ConfirmBuild()
    {
        confirmButtons.SetActive(false);
        spot.Build();
    }

    public void SelfDestoy()
    {
        spot.ClearSpot();
    }
}
