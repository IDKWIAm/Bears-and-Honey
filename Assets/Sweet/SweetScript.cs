using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetScript : MonoBehaviour
{
    public float longPressTime = 2.0f;
    public float pressTimer = 0f;
    public bool isPressed = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            pressTimer = 0f;
        }
        if (isPressed)
        {
            pressTimer += Time.deltaTime;

            if (pressTimer >= longPressTime)
            {
                Destroy(gameObject);
                isPressed = false; 
            }
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            isPressed = false;
        }
    }
}
