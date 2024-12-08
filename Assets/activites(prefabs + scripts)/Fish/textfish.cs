using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textfish : MonoBehaviour
{
    public cameraforfishing CMF;
    public TMP_Text textComponent;
    public TMP_Text textComponent2;
    public void FixedUpdate()
    {
        textComponent.text = "Attempts left: " + CMF.attemptsRemaining;
        int timeregen2 = (int)CMF.timeregen;
        string v = "Time to recovery: " + timeregen2 + "s";
        textComponent2.text = v;
    }
}
