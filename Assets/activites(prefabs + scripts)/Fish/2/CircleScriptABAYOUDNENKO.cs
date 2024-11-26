using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CircleScriptABAYOUDNENKO : MonoBehaviour
{
    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [SerializeField] private Image circle = null;

    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;

    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldUpdate = false;
            indicatorTimer -= 1.0f;
            circle.enabled = true;
            circle.fillAmount = indicatorTimer;

            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                circle.fillAmount = maxIndicatorTimer;
                circle.enabled = false;
                myEvent.Invoke();
            }
        }
        else
        {
            shouldUpdate = true;
            if(shouldUpdate)
            {
                indicatorTimer += 0.1f * Time.deltaTime;
                circle.fillAmount = indicatorTimer;
                if(indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    circle.fillAmount = maxIndicatorTimer;
                    circle.enabled = false;
                    shouldUpdate = false;
                }
            }
        }
        if (Input.GetKeyUp(selectKey))
        {
            shouldUpdate = true;
        }
    }
   
}
