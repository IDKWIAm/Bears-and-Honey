using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SweetScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float targetY;       
    public float destroyY;      
    public float dragSpeed = 5f; 
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private bool dragging;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("PreciseVerticalDragAndDestroy requires a RectTransform!");
            enabled = false;
        }
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            float deltaY = eventData.delta.y * dragSpeed * Time.deltaTime;
            Vector2 newPos = rectTransform.anchoredPosition;
            newPos.y = Mathf.Max(targetY, newPos.y + deltaY); 
            rectTransform.anchoredPosition = newPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        float currentY = rectTransform.anchoredPosition.y;

        if (currentY <= destroyY)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(SmoothReturnToStart());
        }
    }

    System.Collections.IEnumerator SmoothReturnToStart()
    {
        float elapsedTime = 0;
        float returnDuration = 0.2f;
        Vector2 targetPos = startPosition;
        Vector2 startPos = rectTransform.anchoredPosition;

        while (elapsedTime < returnDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / returnDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
    }
}
