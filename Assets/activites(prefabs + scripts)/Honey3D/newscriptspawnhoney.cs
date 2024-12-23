using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class newscriptspawnhoney : MonoBehaviour
{
    public float honeyCooldown;
    public GameObject prefab;          // ������ ��� ������������
    public Transform[] spawnPoints;    // ������ ����� ��� ������
    public string honeyTag = "honey"; // ��� ��� �������� ������� �������� ��������
    public int count = 1;
    public GameObject minigame;

    public int completeReward;
    public DraggableCamera draggableCamera;
    public MinigamesManager minigamesManager;

    [HideInInspector] public float timer { get; private set; }

    private void Start()
    {
        minigamesManager = GameObject.FindObjectOfType<MinigamesManager>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (prefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("������ �/��� ����� ������ �� ������!");
            return;
        }
        if (count == 0)
        {
            FinishMinigame(true);
            if (draggableCamera != null)
                draggableCamera.AllowMovement(true);
            count = 1;
            timer = 60;
            minigame.gameObject.SetActive(false);
        }

        if (!HasHoneyChildren())
        {
            SpawnPrefabs();

        }

    }


    bool HasHoneyChildren()
    {
        foreach (Transform child in transform.GetChild(0))
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
      
        int numObjects = UnityEngine.Random.Range(15, 25);

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
                GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform.GetChild(0));

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

    public void FinishMinigame(bool giveReward)
    {
        if (minigamesManager != null)
            minigamesManager.FinishHoneyMinigame(giveReward);
    }
}

