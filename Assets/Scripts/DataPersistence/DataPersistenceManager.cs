using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private bool saveOnQuit = true;
    [SerializeField] private bool useEncryption;

    [SerializeField] private WebRequestManager requestManager;

    private string slot1Name = "Slot1Data";
    private string slot2Name = "Slot2Data";
    private string slot3Name = "Slot3Data";
    private string saveFormat = ".game";

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler slot1DataHandler;
    private FileDataHandler slot2DataHandler;
    private FileDataHandler slot3DataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;

        slot1DataHandler = new FileDataHandler(Application.persistentDataPath, slot1Name + saveFormat, useEncryption);
        slot2DataHandler = new FileDataHandler(Application.persistentDataPath, slot2Name + saveFormat, useEncryption);
        slot3DataHandler = new FileDataHandler(Application.persistentDataPath, slot3Name + saveFormat, useEncryption);

        dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Loaded slot number"))
            LoadGame(PlayerPrefs.GetInt("Loaded slot number"));
        else Debug.Log("Key 'Loaded slot number' is not found. Load is skipped.");
    }

    public void NewGame(string saveName, int slotNumber)
    {
        gameData = new GameData(saveName);
        if (slotNumber == 1 && slot1DataHandler != null) slot1DataHandler.Create(gameData);
        else if (slotNumber == 2 && slot2DataHandler != null) slot2DataHandler.Create(gameData);
        else if (slotNumber == 3 && slot3DataHandler != null) slot3DataHandler.Create(gameData);
        else Debug.Log("Cannon create a new file. Slot is not found.");

        if (requestManager != null)
            StartCoroutine(requestManager.SendPostRequest(Environment.MachineName + " " + saveName, 0, new List<string>(), new List<string>(), new Dictionary<string, string>()));
        else Debug.Log("Request manager is null");
    }

    public void LoadGame(int slotNumber)
    {
        if (slotNumber == 1 && slot1DataHandler != null) gameData = slot1DataHandler.Load();
        else if (slotNumber == 2 && slot2DataHandler != null) gameData = slot2DataHandler.Load();
        else if (slotNumber == 3 && slot3DataHandler != null) gameData = slot3DataHandler.Load();
        else return;

        if (gameData == null)
        {
            Debug.Log("No data found to load (Slot number is " + slotNumber + ")");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame(string saveName, int slotNumber)
    {
        if (gameData == null)
        {
            Debug.Log("No data found to save (Slot number is " + slotNumber + ")");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        if (slotNumber == 1 && slot1DataHandler != null)
        {
            slot1DataHandler.Delete(slot1Name + saveFormat);
            slot1DataHandler.Create(gameData);
        }
        else if (slotNumber == 2 && slot2DataHandler != null)
        {
            slot2DataHandler.Delete(slot2Name + saveFormat);
            slot2DataHandler.Create(gameData);
        }
        else if (slotNumber == 3 && slot3DataHandler != null)
        {
            slot3DataHandler.Delete(slot3Name + saveFormat);
            slot3DataHandler.Create(gameData);
        }

        if (requestManager != null)
            StartCoroutine(requestManager.SendPutRequest(Environment.MachineName + " " + saveName,
                gameData.resources.energyHoney, gameData.resources.hats, gameData.resources.dishes, gameData.resources.spotsData));
        else print("Request Manager is null");
    }

    public void DeleteGame(string saveName, int slotNumber)
    {
        if (slotNumber == 1 && slot1DataHandler != null) slot1DataHandler.Delete(slot1Name + saveFormat);
        else if (slotNumber == 2 && slot2DataHandler != null) slot2DataHandler.Delete(slot2Name + saveFormat);
        else if (slotNumber == 3 && slot3DataHandler != null) slot3DataHandler.Delete(slot3Name + saveFormat);
        if (requestManager != null) StartCoroutine(requestManager.SendDeleteRequest(Environment.MachineName + " " + saveName));
        else Debug.Log("Request manager is null");
    }

    private void OnApplicationQuit()
    {
        if (saveOnQuit)
        {
            if (PlayerPrefs.HasKey("Loaded slot name"))
            {
                try
                {
                    for (int i = 1; i < 4; i++)
                    {
                        SaveGame(PlayerPrefs.GetString("Loaded slot name"), i);
                        PlayerPrefs.DeleteAll();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to save the game" + "\n" + e);
                }
            }
            else
            {
                Debug.Log("'Loaded slot name' is not found. Save is skipped.");
            }
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
