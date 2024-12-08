using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Cinemachine;
using Unity.VisualScripting;

public class Fishing : MonoBehaviour
{
    public GameObject fish;
    public Animator animator;
    public cameraforfishing script1;
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public CinemachineVirtualCamera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;

    private void Start()
    {
        fish.SetActive(false);
    }
    public void Update()
    {
        if (script1.attemptsRemaining == 0 && !script1.ingame)
        {
            Virtual_cum_shortdistance.gameObject.SetActive(false);
            UI_MINIGAME.gameObject.SetActive(false);
            Virtual_cum_longdistance.gameObject.SetActive(true);
            script1.fishing.gameObject.SetActive(false);
        }

    }
    public void StartFishing()
    {
        StartCoroutine(FishingChallenge());
    }

    private IEnumerator FishingChallenge()
    {
        yield return new WaitForSeconds(Random.Range(4, 8));

        // Начало challenge
        float challengeTime = Time.time + 2f;
        Debug.Log("Нажмите кнопку!");
        animator.SetTrigger("Ulov");


        while (Time.time < challengeTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnSuccess();
                yield break;
            }
            yield return null;
        }

        OnFailure();
    }

    private void OnSuccess()
    {
        fish.SetActive(true);
        animator.SetTrigger("Good");
        Debug.Log("Улов успешен!");
        script1.isRegenerating = true;
        Invoke("Zanovo", 2f);
    }

    private void OnFailure()
    {
        fish.SetActive(false);
        animator.SetTrigger("Bad");
        Debug.Log("Неудача!");
        script1.isRegenerating = true;
        Invoke("Zanovo", 2f);
    }
    public void Zanovo()
    {
        script1.fishing.gameObject.SetActive(false);
        script1.ingame = false;
        fish.SetActive(false);
    }
}




