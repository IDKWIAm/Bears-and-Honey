using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] private float speed = 0.45f;

    [SerializeField] Vector2 xBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);
    [SerializeField] Vector2 zBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    private Vector3 lastDragPosition;

    void Update()
    {
        UpdateDrag();
    }

    private void UpdateDrag()
    {
        if (Input.GetButtonDown("Fire1")) lastDragPosition = Input.mousePosition;
        if (Input.GetButton("Fire1"))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * speed);
            CheckBorders();
            lastDragPosition = Input.mousePosition;
        }
    }

    private void CheckBorders()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xBorders.x, xBorders.y), 
            transform.position.y, Mathf.Clamp(transform.position.z, zBorders.x, zBorders.y));
    }
}
