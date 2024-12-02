using UnityEngine;

public class ShootRayDestroyHoney : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие левой кнопки мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции мыши
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Проверяем пересечение с коллайдером
            {
                // Проверяем тег объекта
                if (hit.collider.CompareTag("honey"))
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("aaaaa");
                }
            }
        }
    }
}
