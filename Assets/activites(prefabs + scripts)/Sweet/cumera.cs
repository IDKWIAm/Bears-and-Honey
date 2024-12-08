using UnityEngine;
using Cinemachine;
public class cumera : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public Camera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;
    public int index_camera;
    private CollectAndRespawn CAR;

    void Start()
    {
        minigamesManager = GameObject.FindObjectOfType<MinigamesManager>();
        Virtual_cum_longdistance = Camera.main;
        CAR = gameObject.GetComponent<CollectAndRespawn>();
        if (CAR == null)
        {
            Debug.LogError("Компонент CollectAndRespawn не найден на этом объекте!");
            enabled = false; // Отключить скрипт, если компонент не найден
            return;
        }
    }

    // Update is called once per frame
    
    public void ActivateGame()
    {
        CAR = gameObject.GetComponent<CollectAndRespawn>();
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
        minigamesManager.FinishBerryMinigame(false);
    }
}
