using UnityEngine;

public class ShootRayDestroyHoney : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��������� ������� ����� ������ ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������� ����
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ��������� ����������� � �����������
            {
                // ��������� ��� �������
                if (hit.collider.CompareTag("honey"))
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("aaaaa");
                }
            }
        }
    }
}
