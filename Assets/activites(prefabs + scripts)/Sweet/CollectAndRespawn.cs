using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Cinemachine;
using System;

public class CollectAndRespawn : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public Transform[] spawnPoints;
    public GameObject prefab;
    public Transform parent;
    public string collectibleTag;
    public Transform parentObject;
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public Camera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;
    public float time = 0.0f;
    [NonSerialized] public int updateEnabled = 0;
    private cumera cumera;
    private static Dictionary<int, int> collectedCounts = new Dictionary<int, int>();

    private List<GameObject> collectedObjects = new List<GameObject>();


    private void Start()
    {
        cumera = GetComponent<cumera>();
        minigamesManager = GameObject.FindObjectOfType<MinigamesManager>();
        Virtual_cum_longdistance = Camera.main;
        int instanceID = gameObject.GetInstanceID();
        if (!collectedCounts.ContainsKey(instanceID))
        {
            collectedCounts[instanceID] = 0;
        }
    }

    void Update()
    {
        if (updateEnabled == 1)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Virtual_cum_shortdistance.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag(collectibleTag))
                {
                    CollectObject(hit.collider.gameObject);
                    break;
                }
            }
        }
    }

    void CollectObject(GameObject obj)
    {
        int instanceID = gameObject.GetInstanceID();

        collectedObjects.Add(obj);
        Destroy(obj);
        collectedCounts[instanceID]++;

        if (collectedCounts[instanceID] == 6)
        {
            Virtual_cum_shortdistance.gameObject.SetActive(false);
            UI_MINIGAME.gameObject.SetActive(false);
            Virtual_cum_longdistance.gameObject.SetActive(true);
            cumera.enabled = true;
            updateEnabled = 0;
            minigamesManager.FinishBerryMinigame(true);

            StartCoroutine(RespawnObjects());
        }
    }

    IEnumerator RespawnObjects()
    {
        yield return new WaitForSeconds(time);
        int instanceID = gameObject.GetInstanceID();
        List<Transform> availableSpawnPoints = spawnPoints.ToList();
        foreach (GameObject obj in collectedObjects)
        {
            if (availableSpawnPoints.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];
                GameObject newObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, parent);
                newObject.transform.parent = parentObject;
                availableSpawnPoints.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("������������ ����� ������!");
                break;
            }
        }
        collectedObjects.Clear();
        collectedCounts[instanceID] = 0;

    }
}
