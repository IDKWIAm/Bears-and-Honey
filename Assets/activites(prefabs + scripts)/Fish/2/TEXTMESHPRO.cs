using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEXTMESHPRO : MonoBehaviour
{
    public TMP_Text percentageText;
    private circlescript circlescript;
    void Start()
    {
        percentageText.text = "0%";
        circlescript = FindObjectOfType<circlescript>();
    }

    
    void Update()
    {
        percentageText.text = Mathf.RoundToInt(circlescript.currentProgress * 100) + "%";
    }
}
