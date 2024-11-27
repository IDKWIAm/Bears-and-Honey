using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safezone : MonoBehaviour
{
    public RectTransform Rect_Transform;
    public GameObject gameobj;
    
    void Start()
    {
        Rect_Transform.anchoredPosition = new Vector2(Random.Range(-284, 284), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
