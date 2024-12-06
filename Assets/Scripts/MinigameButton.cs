using UnityEngine;

public class MinigameButton : MonoBehaviour
{
    private BuildingChoose buildingChoose;

    private void Start()
    {
        buildingChoose = GameObject.FindObjectOfType<BuildingChoose>();
    }

    public void ActivateSongsList()
    {
        buildingChoose.songsList.SetActive(true);
    }
}
