using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SinglePrefabPlacement : MonoBehaviour
{
    public GameObject prefab;       // Префаб для клонирования
    public Transform[] points;     // Массив точек размещения


    void Start()
    {
        // Проверка на наличие префаба и точек
        if (prefab == null || points == null || points.Length == 0)
        {
            Debug.LogError("Необходимо указать префаб и точки в инспекторе!");
            return;
        }

        // Количество объектов (15-25)
        int numObjects = Random.Range(15, 26); // 26 - чтобы включить 25

        // Список доступных точек
        List<Transform> availablePoints = points.ToList();

        // Создание объектов
        for (int i = 0; i < numObjects; i++)
        {
            if (availablePoints.Count > 0)
            {
                // Случайный выбор точки
                int randomIndex = Random.Range(0, availablePoints.Count);
                Transform selectedPoint = availablePoints[randomIndex];

                // Создание объекта
                GameObject newObject = Instantiate(prefab, selectedPoint.position, Quaternion.Euler(-90f, 0f, 31.496f));

                // Удаление точки из списка доступных
                availablePoints.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("Недостаточно точек для размещения всех объектов.");
                break; // Прерываем цикл, если точек не осталось
            }
        }
    }
}
