using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

public class newscriptspawnhoney : MonoBehaviour
{
    public GameObject prefab;          // ������ ��� ������������
    public Transform[] spawnPoints;    // ������ ����� ��� ������
    public string honeyTag = "honey"; // ��� ��� �������� ������� �������� ��������
    public int count = 1;
    public GameObject minigame;

    public int completeReward;
    public DraggableCamera draggableCamera;
    public InventoryManager inventoryManager;

    void Update()
    {
        if (prefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("������ �/��� ����� ������ �� ������!");
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

        // �������� ��������
        for (int i = 0; i < numObjects; i++)
        {
            if (availablePoints.Count > 0)
            {
                // ��������� ����� ����� ������
                int randomIndex = UnityEngine.Random.Range(0, availablePoints.Count);
                Transform spawnPoint = availablePoints[randomIndex];

                // �������� �������
                GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

                // ��������� �������� (������ ���������� �������� ��������)
                newObject.transform.parent = transform;


                // �������� ����� �� ������ ���������
                availablePoints.RemoveAt(randomIndex);

                
            }
            else
            {
                Debug.LogWarning("������������ ����� ������ ��� ���������� ���� ��������.");
                break;
            }
        }

    }
}

