using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textfish : MonoBehaviour
{
    public cameraforfishing CMF;
    public TMP_Text textComponent;
    public void FixedUpdate()
    {
        textComponent.text = "Attempts left: " + CMF.attemptsRemaining;
    }
}
