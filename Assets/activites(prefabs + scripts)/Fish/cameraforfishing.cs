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
    public int attemptsRemaining = 5;
    public bool isRegenerating = false;
    public float regenerationTime = 60f;
    public bool ingame = false;
    public float timeregen = 30f;
    bool timetime = false;

    private void OnMouseDown()
    {
        if (ingame == false)
        {
            TryAttempt();
        }
        ingame = true;
    }
    public void FixedUpdate()
    {
        if (timetime)
        {
            timeregen -= 1 * Time.deltaTime;
        }
    }

    public void TryAttempt()
    {
        if (attemptsRemaining > 0)
        {
            attemptsRemaining--;
            Debug.Log("- �������");
            SwitchCamera();
        }
        if (attemptsRemaining < 5 && isRegenerating == true)
        {
            Debug.Log("���� ��������������");
            StartCoroutine(RegenerateAttempt());
            timetime = true;
        }
    }

    IEnumerator RegenerateAttempt()
    {
        isRegenerating = true;
        Debug.Log("�������������� �������...");
        while (attemptsRemaining != 5)
        {
            yield return new WaitForSeconds(regenerationTime);
            attemptsRemaining++;
            Debug.Log("������� �������������! �������� �������: " + attemptsRemaining);
            timeregen = 0;
        }
        if (attemptsRemaining == 5)
        {
            isRegenerating = false;
            timetime = false;
        }
    }

    public void SwitchCamera()
    {
        Virtual_cum_shortdistance.gameObject.SetActive(true);
        UI_MINIGAME.gameObject.SetActive(true);
        Virtual_cum_longdistance.gameObject.SetActive(false);
        fishing.gameObject.SetActive(true);
    }
}
