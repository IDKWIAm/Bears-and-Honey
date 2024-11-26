using UnityEngine;
using UnityEngine.UI;

public class circlescript : MonoBehaviour
{
    public Image progressBar; // Изображение прогресс-бара
    public float progressSpeed = 1f; // Скорость уменьшения прогресса
    private float currentProgress = 0f;

    void Start()
    {
        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = 0; // Изначально прогресс 0
        progressBar.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Проверяем, удерживается ли кнопка мыши
        {
            
            progressBar.enabled = true;
           
            currentProgress += 1f * Time.deltaTime; // Увеличиваем прогресс
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // Ограничиваем значение
        }
        else // Если кнопка мыши отпущена
        {
            currentProgress -= 0.5f * Time.deltaTime; // Уменьшаем прогресс
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // Ограничиваем значение
        }

        progressBar.fillAmount = currentProgress; // Обновляем визуализацию

        // Если прогресс вернулся на 0, скрываем прогресс-бар
        if (currentProgress <= 0)
        {
            progressBar.enabled = false;
        }
        if (currentProgress >= 1)
        {
            progressBar.gameObject.SetActive(false);
            progressBar.fillAmount = 0;
        }
    }
    
}
