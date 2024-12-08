using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
    public GameObject game;
    public void OnCLick()
    {
        game.SetActive(true);
    }
}
