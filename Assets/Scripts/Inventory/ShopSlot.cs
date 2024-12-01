using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string hatName;

    [SerializeField] private GameObject shop;

    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private DataPersistenceManager persistenceManager;

    private bool bought;

    public void LoadData(GameData data)
    {
        if (data.resources.hats != null)
        {
            foreach (string savedHatName in data.resources.hats)
            {
                if (savedHatName == hatName)
                {
                    button.SetActive(false);
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (!bought) return;

        if (data.resources.hats == null) data.resources.hats = new List<string>() { };

        foreach (string hat in data.resources.hats)
        {
            if (hat != hatName)
            {
                continue;
            }
            return;
        }
        data.resources.hats.Add(hatName);
    }

    private void Save()
    {
        if (PlayerPrefs.HasKey("Loaded slot number"))
        {
            persistenceManager.SaveGame(PlayerPrefs.GetString("Loaded slot name"), PlayerPrefs.GetInt("Loaded slot number"));
        }
        else Debug.Log("Loaded slot number not found. Save is skipped.");
    }


    public void Buy()
    {
        int price = int.Parse(priceText.text);
        if (price <= inventoryManager.currency)
        {
            bought = true;
            inventoryManager.SubtractCurrency(price);
            button.SetActive(false);
            Save();
        }
    }
}
