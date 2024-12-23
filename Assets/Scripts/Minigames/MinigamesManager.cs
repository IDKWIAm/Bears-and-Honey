using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] InventoryManager inventoryManager;

    public GameObject songsList;

    [SerializeField] private DraggableCamera draggableCamera;

    [SerializeField] private int berryCompleteReward = 100;
    [SerializeField] private int honeyCompleteReward = 100;
    [SerializeField] private int fishCompleteReward = 100;
    
    public void ActivateHoneyMinigame()
    {
        draggableCamera.AllowMovement(false);
        canvas.SetActive(false);
    }

    public void FinishHoneyMinigame(bool giveReward = false)
    {
        if (giveReward) inventoryManager.AddCurrency(honeyCompleteReward);
        draggableCamera.AllowMovement(true);
        canvas.SetActive(true);
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

    public void GiveRewardFishMinigame()
    {
        inventoryManager.AddCurrency(fishCompleteReward);
    }

    public void CloseFishMinigame()
    {
        draggableCamera.AllowMovement(true);
        canvas.SetActive(true);
    }
}
