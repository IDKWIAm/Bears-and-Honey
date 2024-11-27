using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private bool saveOnQuit = true;

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

        slot1DataHandler = new FileDataHandler(Application.persistentDataPath, slot1Name + saveFormat);
        slot2DataHandler = new FileDataHandler(Application.persistentDataPath, slot2Name + saveFormat);
        slot3DataHandler = new FileDataHandler(Application.persistentDataPath, slot3Name + saveFormat);

        dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    public void NewGame(string saveName, int slotNumber)
    {
        gameData = new GameData(saveName);
        if (slotNumber == 1 && slot1DataHandler != null) slot1DataHandler.Create(gameData);
        else if (slotNumber == 2 && slot2DataHandler != null) slot2DataHandler.Create(gameData);
        else if (slotNumber == 3 && slot3DataHandler != null) slot3DataHandler.Create(gameData);
        else Debug.Log("Cannon create a new file. Slot is not found.");
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

    public void SaveGame(int slotNumber)
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
    }

    public void DeleteGame(int slotNumber)
    {
        if (slotNumber == 1 && slot1DataHandler != null) slot1DataHandler.Delete(slot1Name + saveFormat);
        else if (slotNumber == 2 && slot2DataHandler != null) slot2DataHandler.Delete(slot2Name + saveFormat);
        else if (slotNumber == 3 && slot3DataHandler != null) slot3DataHandler.Delete(slot3Name + saveFormat);
    }

    private void OnApplicationQuit()
    {
        if (saveOnQuit)
        {
            try 
            {
                for (int i = 1; i < 4; i++)
                {
                    SaveGame(i);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save the game" + "\n" + e);
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
