using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int maxBuiltStructures;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private GameObject buildingsMenu;

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private BuildingPlacement buildingPlacementManager;

    [SerializeField] private InventoryManager inventoryManager;

    private int builtStructures;

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

    private void Start()
    {
        UpdateAmountText();
    }

    private void UpdateAmountText()
    {
        amountText.text = builtStructures.ToString() + "/" + maxBuiltStructures.ToString();

        if (builtStructures == maxBuiltStructures)
            buyButton.SetActive(false);
    }

    public void Buy()
    {
        int price = int.Parse(priceText.text);
        if (price <= inventoryManager.currency && builtStructures < maxBuiltStructures)
        {
            inventoryManager.SubtractCurrency(price);
            buildingPlacementManager.SetBuildingSlot(gameObject.GetComponent<BuildingSlot>());
            buildingPlacementManager.ChooseBuilding(buildingPrefab);
            buildingsMenu.SetActive(false);
        }
    }

    public void AddBuiltStructure()
    {
        builtStructures++;
        UpdateAmountText();
    }
}
