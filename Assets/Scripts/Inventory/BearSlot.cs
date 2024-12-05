using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BearSlot : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TextMeshProUGUI bearNameText;

    private InventoryManager inventoryManager;

    private int bearIdx;

    private void Start()
    {
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

        bearNameText.text = "Bear " + (bearIdx + 1).ToString();

        UpdateHats();
    }

    private void OnEnable()
    {
        if (inventoryManager != null)
            UpdateHats();
    }

    private void UpdateHat(string hatName)
    {
        inventoryManager.ChangeHat(hatName, bearIdx);
    }

    public void UpdateHats()
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        TMP_Dropdown.OptionData noneOpton = new TMP_Dropdown.OptionData("None");
        options.Add(noneOpton);

        if (inventoryManager.hats != null)
        {
            foreach (string hat in inventoryManager.hats)
            {
                TMP_Dropdown.OptionData newOpton = new TMP_Dropdown.OptionData(hat);
                options.Add(newOpton);
            }
        }
        dropdown.AddOptions(options);
    }

    public void UpdateValue()
    {
        string hatName = dropdown.options[dropdown.value].text;
        UpdateHat(hatName);
    }

    public void SetBearIdx(int idx)
    {
        bearIdx = idx;
    }
}
