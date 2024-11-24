using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject confirmButtons;

    private BuildingSpot spot;
    public void SetSpot(GameObject gameObject)
    {
        spot = gameObject.GetComponent<BuildingSpot>();

        if (PlayerPrefs.HasKey(PlayerPrefs.GetString("Loaded Save") + " " + spot.gameObject.name + "Building"))
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
