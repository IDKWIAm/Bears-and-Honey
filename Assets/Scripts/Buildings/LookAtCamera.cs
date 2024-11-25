using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
         transform.LookAt(mainCamera.transform);
    }
}