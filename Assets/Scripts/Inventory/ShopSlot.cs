using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private InventoryManager inventoryManager;
    
    public void Buy()
    {
        int price = int.Parse(priceText.text);
        if (price < inventoryManager.currency)
        {
            inventoryManager.SubtractCurrency(price);
            button.SetActive(false);
        }
    }
}
