using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    public Transform pointA; 
    public Transform pointB;
    public RectTransform safeZone;
    public float moveSpeed = 0; 
    private RectTransform pointerTransform;
    private Vector3 targetPosition;
    public GameObject canvas;
    public GameObject canvasAnother;


    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
        moveSpeed = Random.Range(400, 1000);

    }

    void Update()
    {
       
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;

        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;

        }

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            canvas.SetActive(false);
            canvasAnother.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
    void OnEnable()
    {
        moveSpeed = Random.Range(400, 1000);
        safeZone.anchoredPosition = new Vector2(Random.Range(-284, 284), 0);
    }
}

