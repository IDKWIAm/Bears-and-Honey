using UnityEngine;

public class HatsManager : MonoBehaviour
{
    [SerializeField] GameObject[] hats;

    private InventoryManager inventoryManager;

    private GameObject prevHat;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventoryManager>();

        inventoryManager.AddBear(gameObject.GetComponent<HatsManager>());
    }

    public void ChooseHat(int hatNum)
    {
        if (prevHat == null) prevHat = hats[0];
        
        prevHat.SetActive(false);

        if (hatNum != 0) 
        {
            hats[hatNum-1].SetActive(true);

            prevHat = hats[hatNum-1];
        }
    }
}
