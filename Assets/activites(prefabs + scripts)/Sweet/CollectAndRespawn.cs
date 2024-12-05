using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Cinemachine;

public class CollectAndRespawn : MonoBehaviour
{
    public Transform[] spawnPoints; 
    public GameObject prefab; 
    public string collectibleTag;
    public Transform parentObject; 
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public CinemachineVirtualCamera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;
    public float time = 0.0f;

    private int collectedCount = 0;
    private List<GameObject> collectedObjects = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        collectedObjects.Add(obj);
        Destroy(obj);
        collectedCount++;

        if (collectedCount == 6)
        {
            Virtual_cum_shortdistance.gameObject.SetActive(false);
            UI_MINIGAME.gameObject.SetActive(false);
            Virtual_cum_longdistance.gameObject.SetActive(true);
            StartCoroutine(RespawnObjects());
        }
    }

    IEnumerator RespawnObjects()
    {
        yield return new WaitForSeconds(2f);

        List<Transform> availableSpawnPoints = spawnPoints.ToList();
        foreach (GameObject obj in collectedObjects)
        {
            if (availableSpawnPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];
                GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                newObject.transform.parent = parentObject; 
                availableSpawnPoints.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("Недостаточно точек спавна!");
                break;
            }
        }
        collectedObjects.Clear();
        collectedCount = 0;

    }
}
