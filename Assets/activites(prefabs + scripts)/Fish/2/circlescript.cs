using UnityEngine;
using UnityEngine.UI;

public class circlescript : MonoBehaviour
{
    public Image progressBar; // ����������� ��������-����
    public float progressSpeed = 1f; // �������� ���������� ���������
    private float currentProgress = 0f;

    void Start()
    {
        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = 0; // ���������� �������� 0
        progressBar.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // ���������, ������������ �� ������ ����
        {
            
            progressBar.enabled = true;
           
            currentProgress += 1f * Time.deltaTime; // ����������� ��������
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // ������������ ��������
        }
        else // ���� ������ ���� ��������
        {
            currentProgress -= 0.5f * Time.deltaTime; // ��������� ��������
            currentProgress = Mathf.Clamp(currentProgress, 0, 1); // ������������ ��������
        }

        progressBar.fillAmount = currentProgress; // ��������� ������������

        // ���� �������� �������� �� 0, �������� ��������-���
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
