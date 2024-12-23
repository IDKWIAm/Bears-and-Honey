using UnityEngine;

public class ShootRayFromSecondCamera : MonoBehaviour
{
    public Camera secondCamera;

    private newscriptspawnhoney newscriptspawnhoney;
    private void Start()
    {
        newscriptspawnhoney = GetComponent<newscriptspawnhoney>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Проверка на наличие камеры
            if (secondCamera == null) return;

            Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции мыши
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Проверяем пересечение луча с коллайдером
            {
                // Проверяем тег объекта
                if (hit.collider.CompareTag("honey"))
                {
                    Destroy(hit.collider.gameObject); // Удаляем объект
                    newscriptspawnhoney.count--;
                }
            }
        }
    }
}

