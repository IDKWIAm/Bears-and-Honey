using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cumera : MonoBehaviour
{
    public CinemachineVirtualCamera[] Virtual_cum;
    public int index_camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("sweet_cust"))
                {
                    SwitchCamera();
                }
            }
        }
    }
    public void SwitchCamera()
    {
        Virtual_cum[index_camera].gameObject.SetActive(false);
        index_camera++;
        if(index_camera >= Virtual_cum.Length)
        {
            index_camera = 0;
            Virtual_cum[index_camera].gameObject.SetActive(true);
        }
    }
}
