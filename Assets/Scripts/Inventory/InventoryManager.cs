using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject currencyPopupPrefab;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject buildingsMenu;
    [SerializeField] private DataPersistenceManager persistenceManager;
    [SerializeField] private WebRequestManager requestManager;

    [SerializeField] private TextMeshProUGUI currencyText;

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject[] dishes;

    [SerializeField] private TextMeshProUGUI bearsCountText;
    [SerializeField] private GameObject bearSlotPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private List<HatsManager> bears;

    public List<string> hats { get; private set; }

    private int storedDishesAmount;

    private Vector3 gap;

    public int currency { get; private set; }

    private List<GameObject> storedDishes = new List<GameObject>();

    private bool loading;

    private Transform mainCanvas;

    public void LoadData(GameData data)
    {
        loading = true;

        currency = data.resources.energyHoney;
        UpdateCurrencyText();

        if (data.resources.dishes != null)
        {
            foreach (string dish in data.resources.dishes)
            {
                for (int d = 0; d < dishes.Length; d++)
                {
                    if (dish == dishes[d].name)
                        MakeDish(d + 1);
                }
            }
        }

        if (data.resources.hats != null)
        {
            foreach (string hatName in data.resources.hats)
            {
                AddHat(hatName);
            }
        }

        loading = false;
    }
    
    public void SaveData(ref GameData data)
    {
        data.resources.energyHoney = currency;

        if (data.resources.dishes == null) data.resources.dishes = new List<string>() { };
        else data.resources.dishes.Clear();

        for (int i = 0; i < storedDishes.Count; i++)
        {
            foreach (GameObject dish in dishes)
            {
                if (dish.name + "(Clone)" == storedDishes[i].name)
                {
                    data.resources.dishes.Add(dish.name);
                }
            }
        }
    }

    private void Start()
    {
        if (shop != null) shop.SetActive(false);
        if (buildingsMenu != null) buildingsMenu.SetActive(false);
        UpdateBearsCountText();
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }

    private void UpdateCurrencyText()
    {
        int value = currency;
        string valueReductionSymbol = "";

        if (currency > 9999999)
        {
            value = currency / 100000;
            valueReductionSymbol = "B";
        }
        else if (currency > 9999)
        {
            value = currency / 1000;
            valueReductionSymbol = "k";
        }

        currencyText.text = value.ToString() + valueReductionSymbol;
    }

    private void Save()
    {
        if (PlayerPrefs.HasKey("Loaded slot number"))
        {
            persistenceManager.SaveGame(PlayerPrefs.GetString("Loaded slot name"), PlayerPrefs.GetInt("Loaded slot number"));
        }
        else Debug.Log("Loaded slot number not found. Save is skipped.");
    }

    private void SendLog(Dictionary<string, string> resourcesChanged)
    {
        /*
        if (PlayerPrefs.HasKey("Loaded slot number"))
        {
            StartCoroutine(requestManager.SendLog("Made new dish", 
                Environment.MachineName + " " + PlayerPrefs.GetString("Loaded slot name"), resourcesChanged));
        }
        else Debug.Log("Loaded slot number not found. Log not sent.");
        */
    }

    public void MakeDish(int dishNum)
    {
        dishNum = Mathf.Clamp(dishNum, 0, dishes.Length);
        GameObject dish;
        if (dishNum == 0)
            dish = Instantiate(dishes[UnityEngine.Random.Range(0, dishes.Length)], parent);
        else 
            dish = Instantiate(dishes[dishNum-1], parent);

        storedDishes.Add(dish);
        dish.GetComponent<Dish>().SetOrderNumber(storedDishes.Count - 1);
        storedDishesAmount += 1;

        for (int i = 0; i < dishes.Length; i++)
        {
            if (dishes[i].name + "(Clone)" == dish.name)
            {
                dishNum = i + 1;
            }
        }

        if (!loading)
        {
            Save();
            SendLog(new Dictionary<string, string>() { { "Dish added", dishes[dishNum-1].name } });
        }
    }

    public void SellDish(int price, int num)
    {
        string dishName = storedDishes[num].name.Substring(0, storedDishes[num].name.Length - 7);
        SendLog(new Dictionary<string, string>() { { "Energy Honey", "+" + price.ToString() }, { "Dish removed", dishName } });

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

    private void UpdateBearsCountText()
    {
        if (bearsCountText != null)
            bearsCountText.text = "Bears: " + bears.Count;
    }

    public void AddCurrency(int price)
    {
        CurrencyPopup currencyPopup = Instantiate(currencyPopupPrefab, mainCanvas).GetComponent<CurrencyPopup>();
        currencyPopup.SetParameters(price);
        currency += price;
        UpdateCurrencyText();
        Save();
    }

    public void SubtractCurrency(int price)
    {
        currency -= price;
        UpdateCurrencyText();
        Save();
    }

    public void AddBear(HatsManager bear)
    {
        bears.Add(bear);
        UpdateBearsCountText();
        GameObject bearSlot = Instantiate(bearSlotPrefab, container);
        bearSlot.GetComponent<BearSlot>().SetBearIdx(bears.Count - 1);
    }

    public void AddHat(string hat)
    {
        if (hats == null) hats = new List<string>();
        hats.Add(hat);
    }

    public void ChangeHat(string chosenHat, int chosenBearIdx)
    {
        bears[chosenBearIdx].ChooseHat(chosenHat);
    }
}
