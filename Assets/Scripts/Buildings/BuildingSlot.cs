using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int price;
    [SerializeField] private int maxBuiltStructures;
    [SerializeField] private float riseInPriceMult = 1f;
    [SerializeField] private int riseInPriceIfZero = 100;

    [SerializeField] private int page;

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
        UpdatePriceText();
        UpdateAmountText();
        if (page > 1) transform.parent.gameObject.SetActive(false);
    }

    private void UpdateAmountText()
    {
        amountText.text = builtStructures.ToString() + "/" + maxBuiltStructures.ToString();

        if (builtStructures == maxBuiltStructures)
            buyButton.SetActive(false);
    }

    private void UpdatePriceText()
    {
        int value = price;
        string valueReductionSymbol = "";

        if (price > 9999999)
        {
            value = price / 100000;
            valueReductionSymbol = "B";
        }
        else if (price > 9999)
        {
            value = price / 1000;
            valueReductionSymbol = "k";
        }

        priceText.text = value.ToString() + valueReductionSymbol;
    }

    public void Buy()
    {
        if (price <= inventoryManager.currency && builtStructures < maxBuiltStructures)
        {
            buildingPlacementManager.SetBuildingSlot(gameObject.GetComponent<BuildingSlot>());
            buildingPlacementManager.ChooseBuilding(buildingPrefab);
            buildingsMenu.SetActive(false);
        }
    }

    public void AddBuiltStructure()
    {
        builtStructures++;
        UpdateAmountText();
        inventoryManager.SubtractCurrency(int.Parse(priceText.text));
        if (price == 0) price += riseInPriceIfZero;
        else price = (int)((float)price * riseInPriceMult);
        UpdatePriceText();
    }
}
