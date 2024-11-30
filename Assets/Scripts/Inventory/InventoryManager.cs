using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private float dishesGapMult = 1;

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private Transform dishDefaultPos;
    [SerializeField] private Transform parent;

    [SerializeField] private GameObject[] dishes;

    private int storedDishesAmount;

    private Vector3 gap;

    public int currency { get; private set; }

    private List<GameObject> storedDishes = new List<GameObject>();

    private void UpdateCurrencyText()
    {
        int value = currency;
        string valueReductionSymbol = "";

        if (currency > 999999)
        {
            value = currency / 100000;
            valueReductionSymbol = "B";
        }
        else if (currency > 999)
        {
            value = currency / 1000;
            valueReductionSymbol = "k";
        }

        currencyText.text = value.ToString() + valueReductionSymbol;
    }

    public void MakeDish(int dishNum)
    {
        dishNum = Mathf.Clamp(dishNum, 0, dishes.Length);
        gap = new Vector3(dishes[0].GetComponent<RectTransform>().rect.width * dishesGapMult, 0, 0);
        GameObject dish;
        if (dishNum == 0)
            dish = Instantiate(dishes[Random.Range(0, dishes.Length)], dishDefaultPos.position + gap * storedDishesAmount, Quaternion.identity, parent);
        else 
            dish = Instantiate(dishes[dishNum-1], dishDefaultPos.position + gap * storedDishesAmount, Quaternion.identity, parent);

        storedDishes.Add(dish);
        dish.GetComponent<Dish>().SetOrderNumber(storedDishes.Count - 1);
        storedDishesAmount += 1;
    }

    public void MakeCherryDish()
    {
        gap = new Vector3(dishesGapMult, 0, 0);
        GameObject dish = Instantiate(dishes[0], dishDefaultPos.position + gap * storedDishesAmount, Quaternion.identity, parent);
        storedDishes.Add(dish);
        dish.GetComponent<Dish>().SetOrderNumber(storedDishes.Count - 1);
        storedDishesAmount += 1;
    }

    public void SellDish(int price, int num)
    {
        AddCurrency(price);
        storedDishesAmount -= 1;
        storedDishes.Remove(storedDishes[num]);
        UpdateDishesOrder(num);
    }

    private void UpdateDishesOrder(int deletedFargmentNum)
    {
        for (int i = 0; i < storedDishes.Count; i++)
        {
            if (i >= deletedFargmentNum)
            {
                storedDishes[i].transform.position -= gap;
                storedDishes[i].GetComponent<Dish>().SetOrderNumber(i);
            }
        }
    }

    public void AddCurrency(int price)
    {
        currency += price;
        UpdateCurrencyText();
    }

    public void SubtractCurrency(int price)
    {
        currency -= price;
        UpdateCurrencyText();
    }
}
