using System.Linq;
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

    public void ChooseHat(string hatName)
    {
        if (prevHat == null) prevHat = hats[0];
        
        prevHat.SetActive(false);

        if (hatName != "None") 
        {
            for (int i = 0; i < hats.Length; i++)
            {
                if (hats[i].name == hatName)
                {
                    hats[i].SetActive(true);
                    prevHat = hats[i];
                }
            }
        }
    }
}
