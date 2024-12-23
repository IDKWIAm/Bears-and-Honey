using System.Collections;
using UnityEngine;
using Cinemachine;

public class Fishing : MonoBehaviour
{
    public MinigamesManager minigamesManager;
    public GameObject fish;
    public Animator animator;
    public cameraforfishing cameraForFishing;
    public CinemachineVirtualCamera Virtual_cum_shortdistance;
    public Camera Virtual_cum_longdistance;
    public Canvas UI_MINIGAME;

    private void Start()
    {
        Virtual_cum_longdistance = Camera.main;
        minigamesManager = GameObject.FindObjectOfType<MinigamesManager>();
        fish.SetActive(false);
    }
    public void Update()
    {
        if (cameraForFishing.attemptsRemaining == 0 && !cameraForFishing.ingame)
        {
            Virtual_cum_shortdistance.gameObject.SetActive(false);
            UI_MINIGAME.gameObject.SetActive(false);
            Virtual_cum_longdistance.gameObject.SetActive(true);
            cameraForFishing.fishing.gameObject.SetActive(false);
            cameraForFishing.ingame = false;
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
        minigamesManager.GiveRevardFishMinigame();
        fish.SetActive(true);
        animator.SetTrigger("Good");
        Debug.Log("Улов успешен!");
        cameraForFishing.isRegenerating = true;
        Invoke("Zanovo", 2f);
    }

    private void OnFailure()
    {
        fish.SetActive(false);
        animator.SetTrigger("Bad");
        Debug.Log("Неудача!");
        cameraForFishing.isRegenerating = true;
        Invoke("Zanovo", 2f);
    }
    public void Zanovo()
    {
        cameraForFishing.fishing.gameObject.SetActive(false);
        cameraForFishing.ingame = false;
        fish.SetActive(false);
    }
    public void forbutton()
    {
        cameraForFishing.ingame = false;
        cameraForFishing.fishing.gameObject.SetActive(false);
        fish.SetActive(false);
        Virtual_cum_shortdistance.gameObject.SetActive(false);
        UI_MINIGAME.gameObject.SetActive(false);
        Virtual_cum_longdistance.gameObject.SetActive(true);
        minigamesManager.CloseFishMinigame();
    }
}




