using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] InventoryManager inventoryManager;

    public GameObject songsList;

    [SerializeField] private DraggableCamera draggableCamera;

    [SerializeField] private float honeyCooldown;
    [SerializeField] private GameObject honeyMinigame;

    [SerializeField] private int berryCompleteReward = 100;

    [SerializeField] private int fishCompleteReward = 100;

    [HideInInspector] public float honeyMinigameTimer { get; private set; }
    [HideInInspector] public float fishMinigameTimer { get; private set; }
    
    private void Update()
    {
        if (honeyMinigameTimer > 0)
            honeyMinigameTimer -= Time.deltaTime;

        if (fishMinigameTimer > 0)
            fishMinigameTimer -= Time.deltaTime;
    }

    public void ActivateHoneyMinigame()
    {
        draggableCamera.AllowMovement(false);
        honeyMinigame.SetActive(true);
    }

    public void FinishHoneyMinigame()
    {
        honeyMinigameTimer = honeyCooldown;
    }

    public void ActivateBerryMinigame()
    {
        draggableCamera.AllowMovement(false);
        canvas.SetActive(false);
    }

    public void FinishBerryMinigame(bool completed)
    {
        draggableCamera.AllowMovement(true);
        canvas.SetActive(true);
        if (completed) inventoryManager.AddCurrency(berryCompleteReward);
    }

    public void ActivateFishMinigame()
    {
        draggableCamera.AllowMovement(false);
        canvas.SetActive(false);
    }

    public void GiveRevardFishMinigame()
    {
        inventoryManager.AddCurrency(fishCompleteReward);
    }

    public void CloseFishMinigame()
    {
        draggableCamera.AllowMovement(true);
        canvas.SetActive(true);
    }

    public void ResetHoneyTimer()
    {
        honeyMinigameTimer = honeyCooldown;
    }
}
