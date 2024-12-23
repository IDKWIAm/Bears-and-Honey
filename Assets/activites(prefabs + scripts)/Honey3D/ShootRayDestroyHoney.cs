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
            // �������� �� ������� ������
            if (secondCamera == null) return;

            Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������� ����
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ��������� ����������� ���� � �����������
            {
                // ��������� ��� �������
                if (hit.collider.CompareTag("honey"))
                {
                    Destroy(hit.collider.gameObject); // ������� ������
                    newscriptspawnhoney.count--;
                }
            }
        }
    }
}

