using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SweetScript : MonoBehaviour, IPointerDownHandler
{
    public float startPosX;
    public float startPosY;
    public bool isBeingHeld = false;

    void FixedUpdate()
    {
        if (isBeingHeld == true)
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(0, mousePos.y, 0);
        }
    }
    
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {


            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            isBeingHeld = true;
        }
    }
    public void OnPointerUP(PointerEventData eventData)
    {
            isBeingHeld = false;
        
    }
}
