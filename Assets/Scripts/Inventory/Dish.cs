using TMPro;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private int price;

    [SerializeField] private TextMeshProUGUI priceText;

    private InventoryManager inventoryManager;
    private int storedDishesNumber;

    private void Start()
    {
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        priceText.text = price.ToString();
    }

    public void Sell()
    {
        inventoryManager.SellDish(price, storedDishesNumber);
        Destroy(gameObject);
    }

    public void SetOrderNumber(int num)
    {
        storedDishesNumber = num;
    }
}
