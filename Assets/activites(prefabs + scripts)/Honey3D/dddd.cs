using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dddd : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("honey"))
            {
                Debug.Log("������ �������� ������ � ����� \"honey\": " + child.name);
                //��������� �������� � ��������� ��������.
            }
        }
    }
}
