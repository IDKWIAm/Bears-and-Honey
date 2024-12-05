using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cumera : MonoBehaviour
{
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public CinemachineVirtualCamera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;
    public int index_camera;
    private CollectAndRespawn CAR;
    void Start()
    {
        CAR = GetComponent<CollectAndRespawn>();
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
                    CAR.updateEnabled = 1;
                    enabled = false;
                    SwitchCamera();
                }
            }
        }
    }
    public void SwitchCamera()
    {
        Virtual_cum_shortdistance.gameObject.SetActive(true);
        UI_MINIGAME.gameObject.SetActive(true);
        Virtual_cum_longdistance.gameObject.SetActive(false);

    }
    public void FORBUTTON()
    {
        Virtual_cum_shortdistance.gameObject.SetActive(false);
        UI_MINIGAME.gameObject.SetActive(false);
        Virtual_cum_longdistance.gameObject.SetActive(true);
    }
}
