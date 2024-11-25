using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safezone : MonoBehaviour
{
    public RectTransform RectTransform;
    public GameObject gameobj;
    void Start()
    {
        RectTransform.anchoredPosition = new Vector2(Random.Range(-284, 284), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
