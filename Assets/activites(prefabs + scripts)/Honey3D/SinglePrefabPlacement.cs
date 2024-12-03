using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SinglePrefabPlacement : MonoBehaviour
{
    public GameObject prefab;       // ������ ��� ������������
    public Transform[] points;     // ������ ����� ����������


    void Start()
    {
        // �������� �� ������� ������� � �����
        if (prefab == null || points == null || points.Length == 0)
        {
            Debug.LogError("���������� ������� ������ � ����� � ����������!");
            return;
        }
 
    }
    private void Update()
    {
        GameObject[] honeyObjects = GameObject.FindGameObjectsWithTag("honey");

        if (honeyObjects.Length == 0)
        {
            // ���������� �������� (15-25)
            int numObjects = Random.Range(15, 26); // 26 - ����� �������� 25

            // ������ ��������� �����
            List<Transform> availablePoints = points.ToList();

            // �������� ��������
            for (int i = 0; i < numObjects; i++)
            {
                if (availablePoints.Count > 0)
                {
                    // ��������� ����� �����
                    int randomIndex = Random.Range(0, availablePoints.Count);
                    Transform selectedPoint = availablePoints[randomIndex];

                    // �������� �������
                    GameObject newObject = Instantiate(prefab, selectedPoint.position, Quaternion.Euler(0.322f, 0f, -30.21f));

                    // �������� ����� �� ������ ���������
                    availablePoints.RemoveAt(randomIndex);
                }
                else
                {
                    Debug.LogWarning("������������ ����� ��� ���������� ���� ��������.");
                    break; // ��������� ����, ���� ����� �� ��������
                }
            }
        }
       
    }
    private void LateUpdate()
    {
        GameObject[] honeyObjects = GameObject.FindGameObjectsWithTag("honey");
        if (honeyObjects.Length == 0)
        {
            GameObject honeyMinigame = GameObject.FindGameObjectWithTag("honey_minigame");

            if (honeyMinigame != null)
            {
                honeyMinigame.SetActive(false);
            }
        }
    }
}
