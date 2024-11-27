using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class circlescript : MonoBehaviour
{
    public Image progressBar; // Изображение прогресс-бара
    public float progressSpeed = 1f; // Скорость уменьшения прогресса
    [NonSerialized] public float currentProgress = 0f;
    public GameObject canvas;

    void Start()
    {
        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = 0; // Изначально прогресс 0
        progressBar.enabled = false;
        
    }

    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0)) // Проверяем, удерживается ли кнопка мыши
        {
            progressBar.enabled = true;
        }
        else // Если кнопка мыши отпущена
        {
            currentProgress -= 1f * Time.deltaTime; // Уменьшаем прогресс
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // Ограничиваем значение
        }

        progressBar.fillAmount = currentProgress; // Обновляем визуализацию

        // Если прогресс вернулся на 0, скрываем прогресс-бар
        if (currentProgress <= 0)
        {
            progressBar.enabled = false;
        }
        if (currentProgress >= 0.98f)
        {
            progressBar.fillAmount = 0;
            currentProgress = 0;
            canvas.SetActive(false);
        }
    }
    public void clickbutton()
    {
        currentProgress += 4f * Time.deltaTime * progressSpeed;
        currentProgress = Mathf.Clamp(currentProgress, 0, 1);
    }

}
