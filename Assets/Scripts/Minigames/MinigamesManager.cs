using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    public GameObject songsList;

    [SerializeField] private DraggableCamera draggableCamera;

    [SerializeField] private GameObject honeyMinigame;
    [SerializeField] private GameObject berryMinigame;
    [SerializeField] private GameObject fishMinigame;

    public void ActivateHoneyMinigame()
    {
        draggableCamera.AllowMovement(false);
        honeyMinigame.SetActive(true);
    }

    public void ActivateBerryMinigame()
    {
        draggableCamera.AllowMovement(false);
        berryMinigame.SetActive(true);
    }

    public void ActivateFishMinigame()
    {

    }
}
