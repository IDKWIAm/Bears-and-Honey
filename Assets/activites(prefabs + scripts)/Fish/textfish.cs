using TMPro;
using UnityEngine;

public class textfish : MonoBehaviour
{
    [SerializeField] private cameraforfishing CMF;
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private TMP_Text textComponent2;

    public void Update()
    {
        textComponent.text = "Attempts left: " + CMF.attemptsRemaining;
        int timeregen2 = (int)CMF.timeregen;
        string v = "Time to recovery: " + timeregen2 + "s";
        textComponent2.text = v;
    }
}
