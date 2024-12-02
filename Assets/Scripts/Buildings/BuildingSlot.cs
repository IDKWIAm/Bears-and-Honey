using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private GameObject buildingsMenu;

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private BuildingPlacement buildingPlacementManager;

    [SerializeField] private InventoryManager inventoryManager;

    public void LoadData(GameData data)
    {
        if (data.resources.spotsData != null)
        {
            foreach (KeyValuePair<string, string> valuePair in data.resources.spotsData)
            {
                if (valuePair.Value == buildingNameText.text)
                {
                    buyButton.SetActive(false);
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        // nothing to save
    }

    public void Buy()
    {
        int price = int.Parse(priceText.text);
        if (price <= inventoryManager.currency)
        {
            inventoryManager.SubtractCurrency(price);
            buildingPlacementManager.SetBuyButton(buyButton);
            buildingPlacementManager.ChooseBuilding(buildingPrefab);
            buildingsMenu.SetActive(false);
        }
    }
}
