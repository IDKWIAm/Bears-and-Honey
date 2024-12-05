using UnityEngine;

public class ShootRayFromSecondCamera : MonoBehaviour
{

    private newscriptspawnhoney newscriptspawnhoney;
    private void Start()
    {
        newscriptspawnhoney = GetComponent<newscriptspawnhoney>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // ������� ������ � ����� "secondcamera"
            Camera secondCamera = GameObject.FindGameObjectWithTag("secondcamera").GetComponent<Camera>();

            // �������� �� ������� ������
            if (secondCamera == null)
            {
                Debug.LogError("������ � ����� \"secondcamera\" �� �������!");
                return;
            }

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

