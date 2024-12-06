using BearAI;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] int dishNum;
    [SerializeField] GameObject confirmButtons;
    [SerializeField] GameObject minigameButton;
    [SerializeField] GameObject bear;

    private BuildingSpot spot;
    public void SetSpot(GameObject gameObject)
    {
        spot = gameObject.GetComponent<BuildingSpot>();
    }

    public void ConfirmBuild()
    {
        spot.Build();
        confirmButtons.SetActive(false);
        if (bear != null)
        {
            bear.SetActive(true);
            bear.GetComponent<BearBlackAI>()?.SetDish(dishNum);
        }
    }

    public void SelfDestoy()
    {
        spot.ClearSpot();
    }

    public void activateMinigameButton()
    {
        if (minigameButton != null && confirmButtons.activeSelf == false)
            minigameButton.SetActive(true);
    }
}
