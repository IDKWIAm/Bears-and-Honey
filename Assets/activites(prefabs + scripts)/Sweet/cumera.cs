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
        if (CAR == null)
        {
            Debug.LogError("Компонент CollectAndRespawn не найден на этом объекте!");
            enabled = false; // Отключить скрипт, если компонент не найден
            return;
        }
    }

    // Update is called once per frame
    
    private void OnMouseDown()
    {
        CAR.updateEnabled = 1;
        enabled = false;
        SwitchCamera();
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
