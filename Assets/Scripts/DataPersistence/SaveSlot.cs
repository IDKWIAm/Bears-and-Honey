using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject ñreateButton;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject deleteButton;

    [SerializeField] private DataPersistenceManager persistenceManager;

    private string savedTitle;
    private int slotNum;

    public void LoadData(GameData data)
    {
        savedTitle = data.name;
    }

    public void SaveData(ref GameData data)
    {
        data.name = title.text;
    }

    private void Start()
    {
        if (gameObject.name == "Save Slot 1") slotNum = 1;
        else if (gameObject.name == "Save Slot 2") slotNum = 2;
        else if (gameObject.name == "Save Slot 3") slotNum = 3;
        else throw new Exception("Cannot find any slots to load (Maybe their names are not correct?)");

        persistenceManager.LoadGame(slotNum);
        if (title.text == savedTitle)
        {
            loadButton.SetActive(true);
            deleteButton.SetActive(true);
            ñreateButton.SetActive(false);
        }
        else
        {
            loadButton.SetActive(false);
            deleteButton.SetActive(false);
            ñreateButton.SetActive(true);
        }
    }

    public void CreateSave()
    {
        persistenceManager.NewGame(title.text, slotNum);
        loadButton.SetActive(true);
        deleteButton.SetActive(true);
        ñreateButton.SetActive(false);
    }

    public void LoadSave()
    {
        PlayerPrefs.SetString("Loaded slot name", title.text);
        PlayerPrefs.SetInt("Loaded slot number", slotNum);
        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        persistenceManager.DeleteGame(title.text, slotNum);
        PlayerPrefs.DeleteKey("Loaded slot name");
        PlayerPrefs.DeleteKey("Loaded slot number");
        loadButton.SetActive(false);
        deleteButton.SetActive(false);
        ñreateButton.SetActive(true);

    }
}
