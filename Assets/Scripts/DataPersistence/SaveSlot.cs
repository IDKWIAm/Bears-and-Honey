using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class SaveSlot : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject ñreateButton;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject deleteButton;

    [SerializeField] private WebRequestManager requestManager;
    [SerializeField] private DataPersistenceManager persistenceManager;

    private string gameURL = "https://2025.nti-gamedev.ru/api/games/de781ac2-27da-479a-b5f1-f572f8c9aacb/";

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
        if (requestManager != null) StartCoroutine(requestManager.SendPostRequest(gameURL + "players/", Environment.MachineName + " " + title.text, 0, 0, new List<string>(), new Dishes(), new BuildingSpotData()));
        else Debug.Log("Request manager is null");
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
        if (requestManager != null) StartCoroutine(requestManager.SendDeleteRequest(gameURL + "players/" + Environment.MachineName + " " + title.text + "/"));
        else Debug.Log("Request manager is null");
        persistenceManager.DeleteGame(slotNum);
        PlayerPrefs.DeleteKey("Loaded slot name");
        PlayerPrefs.DeleteKey("Loaded slot number");
        loadButton.SetActive(false);
        deleteButton.SetActive(false);
        ñreateButton.SetActive(true);

    }
}
