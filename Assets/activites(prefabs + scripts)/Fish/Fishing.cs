using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
public class Fishing : MonoBehaviour
{
    public Animator animator; // ���������� Animator � GameObject
    public GameObject fishObject; // GameObject ����

    public int attemptsRemaining = 5;
    public int maxAttempts = 5;
    private bool success;
    private bool isRegenerating = false;
    private float regenerationTime = 60f; // 1 ������

    public void StartFishing()
    {
        if (attemptsRemaining > 0 && !isRegenerating)
        {
            StartCoroutine(FishingProcess());
            attemptsRemaining--;
        }
        else if (isRegenerating)
        {
            Debug.Log("������� �����������������...");
        }
        else
        {
            Debug.Log("������� �����������! ��������� ������� ����� " + regenerationTime + " ������.");
        }
    }


    IEnumerator FishingProcess()
    {
        PlayAnimation(0); // �������� ������
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // ���� ��������� ��������

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
            PlayAnimation(4); // �������� �����
            fishObject.SetActive(true);
        }
        else
        {
            PlayAnimation(3); // �������� �������
        }
        StartCoroutine(EndFishing());
    }

    IEnumerator RegenerateAttempt()
    {
        isRegenerating = true;
        Debug.Log("�������������� �������...");
        yield return new WaitForSeconds(regenerationTime);
        attemptsRemaining++;
        isRegenerating = false;
        if (attemptsRemaining < maxAttempts)
        {
            Debug.Log("������� �������������! �������� �������: " + attemptsRemaining);
        }
    }

    IEnumerator EndFishing()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        PlayAnimation(5); // �������� ������� ������
        fishObject.SetActive(false); // ���� ��������
    }


    void PlayAnimation(int animationIndex)
    {
        animator.SetInteger("AnimationIndex", animationIndex);
    }
}



