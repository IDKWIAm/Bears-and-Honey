using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
public class Fishing : MonoBehaviour
{
    public Animator animator; // Прикрепите Animator к GameObject
    public GameObject fishObject; // GameObject рыбы

    public int attemptsRemaining = 5;
    public int maxAttempts = 5;
    private bool success;
    private bool isRegenerating = false;
    private float regenerationTime = 60f; // 1 минута

    public void StartFishing()
    {
        if (attemptsRemaining > 0 && !isRegenerating)
        {
            StartCoroutine(FishingProcess());
            attemptsRemaining--;
        }
        else if (isRegenerating)
        {
            Debug.Log("Попытка восстанавливается...");
        }
        else
        {
            Debug.Log("Попытки закончились! Следующая попытка через " + regenerationTime + " секунд.");
        }
    }


    IEnumerator FishingProcess()
    {
        PlayAnimation(0); // Анимация старта
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Ждем окончания анимации

        float delay = Random.Range(4f, 8f);
        yield return new WaitForSeconds(delay);

        StartCoroutine(StartChallenge());



        if (attemptsRemaining == 0 && !isRegenerating)
        {
            StartCoroutine(RegenerateAttempt());
        }
    }

    IEnumerator StartChallenge()
    {
        PlayAnimation(2); 
        float challengeTime = 2f;
        float startTime = Time.time;
        bool buttonPressed = false;

        while (Time.time - startTime < challengeTime)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                buttonPressed = true;
                break;
            }
            yield return null;
        }
        yield return buttonPressed;
        bool success = buttonPressed;
        HandleResult(success);
        PlayAnimation(1);
    }

    void HandleResult(bool success)
    {
        if (success)
        {
            PlayAnimation(4); // Анимация удачи
            fishObject.SetActive(true);
        }
        else
        {
            PlayAnimation(3); // Анимация неудачи
        }
        StartCoroutine(EndFishing());
    }

    IEnumerator RegenerateAttempt()
    {
        isRegenerating = true;
        Debug.Log("Восстановление попытки...");
        yield return new WaitForSeconds(regenerationTime);
        attemptsRemaining++;
        isRegenerating = false;
        if (attemptsRemaining < maxAttempts)
        {
            Debug.Log("Попытка восстановлена! Осталось попыток: " + attemptsRemaining);
        }
    }

    IEnumerator EndFishing()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        PlayAnimation(5); // Анимация подъема удочки
        fishObject.SetActive(false); // Рыба исчезает
    }


    void PlayAnimation(int animationIndex)
    {
        animator.SetInteger("AnimationIndex", animationIndex);
    }
}



