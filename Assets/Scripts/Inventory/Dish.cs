using TMPro;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private int price = 10;
    [SerializeField] private float expirationTime = 10f;
    [SerializeField] private float onDiscountTime = 4f;
    [SerializeField] private int discount;
    [SerializeField] private int expirationAdditionalDiscont;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject onDiscountInscription;

    private InventoryManager inventoryManager;
    private int storedDishesNumber;
    private bool onSale;

    private void Start()
    {
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        priceText.text = price.ToString();
    }

    private void Update()
    {
        if (expirationTime > 0)
        {
            if (expirationTime <= onDiscountTime && !onSale)
            {
                onDiscountInscription.SetActive(true);
                price -= (int)((float)price / 100 * discount);
                priceText.text = price.ToString();
                onSale = true;
            }

            expirationTime -= Time.deltaTime;
        }
        else
        {
            price -= (int)((float)price / 100 * expirationAdditionalDiscont);
            Sell();
        }
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
