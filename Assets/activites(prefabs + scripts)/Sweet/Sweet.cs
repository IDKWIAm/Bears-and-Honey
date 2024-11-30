using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("honey"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
