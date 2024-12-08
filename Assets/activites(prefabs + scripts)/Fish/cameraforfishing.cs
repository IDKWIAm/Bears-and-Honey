using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraforfishing : MonoBehaviour
{
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public CinemachineVirtualCamera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;
    public GameObject fishing;
    private void OnMouseDown()
    {
        enabled = false;
        SwitchCamera();
    }
    public void SwitchCamera()
    {
        Virtual_cum_shortdistance.gameObject.SetActive(true);
        UI_MINIGAME.gameObject.SetActive(true);
        Virtual_cum_longdistance.gameObject.SetActive(false);
        Invoke("ffishing", 1f);

    }
    void ffishing()
    {
        fishing.SetActive(true);
    }
}
