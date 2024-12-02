using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close : MonoBehaviour
{
    public void HideHoney()
    {
        GameObject honeyMinigame = GameObject.FindGameObjectWithTag("honey_minigame");

        if (honeyMinigame != null)
        {
            honeyMinigame.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Объект с тегом \"honey_minigame\" не найден.");
        }
    }
}
