using JetBrains.Annotations;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class circlescript : MonoBehaviour
{
    public Image progressBar; // ����������� ��������-����
    public float progressSpeed = 1f; // �������� ���������� ���������
    [NonSerialized] public float currentProgress = 0f;
    

    void Start()
    {
        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = 0; // ���������� �������� 0
        progressBar.enabled = false;
        
    }

    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0)) // ���������, ������������ �� ������ ����
        {
            progressBar.enabled = true;
        }
        else // ���� ������ ���� ��������
        {
            currentProgress -= 1f * Time.deltaTime; // ��������� ��������
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // ������������ ��������
        }

        progressBar.fillAmount = currentProgress; // ��������� ������������

        // ���� �������� �������� �� 0, �������� ��������-���
        if (currentProgress <= 0)
        {
            progressBar.enabled = false;
        }
        if (currentProgress >= 0.99f)
        {
            progressBar.gameObject.SetActive(false);
            progressBar.fillAmount = 0;
        }
    }
    public void clickbutton()
    {
        currentProgress += 2f * Time.deltaTime * progressSpeed;
        currentProgress = Mathf.Clamp(currentProgress, 0, 1);
    }
}