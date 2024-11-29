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
        Vector3 target = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        transform.LookAt(target);
    }
}
