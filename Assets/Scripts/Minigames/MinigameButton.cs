using UnityEngine;

public class MinigameButton : MonoBehaviour
{
    private MinigamesManager minigamesManager;

    private void Start()
    {
        minigamesManager = GameObject.FindObjectOfType<MinigamesManager>();
    }

    public void ActivateSongsList()
    {
        minigamesManager.songsList.SetActive(true);
    }

    public void ActivateHoneyMinigame()
    {
        minigamesManager.ActivateHoneyMinigame();
    }

    public void ActivateBerryMinigame()
    {
        minigamesManager.ActivateBerryMinigame();
    }

    public void ActivateFishMinigame()
    {
        minigamesManager.ActivateFishMinigame();
    }
}
