using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class newscriptspawnhoney : MonoBehaviour
{
    public float honeyCooldown;
    public GameObject prefab;          // Префаб для клонирования
    public Transform[] spawnPoints;    // Массив точек для спавна
    public string honeyTag = "honey"; // Тег для проверки наличия дочерних объектов
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
            Debug.LogError("Префаб и/или точки спавна не заданы!");
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

        // Создание объектов
        for (int i = 0; i < numObjects; i++)
        {
            if (availablePoints.Count > 0)
            {
                // Случайный выбор точки спавна
                int randomIndex = UnityEngine.Random.Range(0, availablePoints.Count);
                Transform spawnPoint = availablePoints[randomIndex];

                // Создание объекта
                GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform.GetChild(0));

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

    public void FinishMinigame(bool giveReward)
    {
        if (minigamesManager != null)
            minigamesManager.FinishHoneyMinigame(giveReward);
    }
}

