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
                GameObject newObject = Instantiate(prefab, selectedPoint.position, Quaternion.Euler(-90f, 0f, 31.496f));

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
