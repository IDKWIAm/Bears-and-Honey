using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public class newscriptspawnhoney : MonoBehaviour
{
    public GameObject prefab;          // Префаб для клонирования
    public Transform[] spawnPoints;    // Массив точек для спавна
    public string honeyTag = "honey"; // Тег для проверки наличия дочерних объектов
    public int count = 1;
    public GameObject minigame;

    public int completeReward;
    public DraggableCamera draggableCamera;
    public InventoryManager inventoryManager;

    void Update()
    {
        if (prefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Префаб и/или точки спавна не заданы!");
            return;
        }
        if (count == 0)
        {
            if (inventoryManager != null)
                inventoryManager.AddCurrency(completeReward);
            if (draggableCamera != null)
                draggableCamera.AllowMovement(true);
            count = 1;
            minigame.gameObject.SetActive(false);
        }

        if (!HasHoneyChildren())
        {
            SpawnPrefabs();

        }

    }


    bool HasHoneyChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(honeyTag))
            {
                return true;
            }
        }
        return false;
    }

    void SpawnPrefabs()
    {
      
        int numObjects = UnityEngine.Random.Range(15, 26);

        count = numObjects;


        
        List<Transform> availablePoints = spawnPoints.ToList();

        // Создание объектов
        for (int i = 0; i < numObjects; i++)
        {
            if (availablePoints.Count > 0)
            {
                // Случайный выбор точки спавна
                int randomIndex = UnityEngine.Random.Range(0, availablePoints.Count);
                Transform spawnPoint = availablePoints[randomIndex];

                // Создание объекта
                GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

                // Установка родителя (префаб становится дочерним объектом)
                newObject.transform.parent = transform;


                // Удаление точки из списка доступных
                availablePoints.RemoveAt(randomIndex);

                
            }
            else
            {
                Debug.LogWarning("Недостаточно точек спавна для размещения всех объектов.");
                break;
            }
        }

    }
}

