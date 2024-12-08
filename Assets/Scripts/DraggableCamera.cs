using UnityEngine;

public class DraggableCamera : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 0.45f;

    [SerializeField] Vector2 xBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);
    [SerializeField] Vector2 zBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    [SerializeField] float zoomStrength = 1f;

    [SerializeField] Vector2 zoomBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    private Vector3 lastDragPosition;

    public bool isMovingAllowed { get; private set; } = true;

    void Update()
    {
        if (!isMovingAllowed) return;

        UpdateDrag();
        UpdateZoom();
    }

    private void UpdateDrag()
    {
        if (Input.GetButtonDown("Fire1")) lastDragPosition = Input.mousePosition;
        if (Input.GetButton("Fire1"))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * dragSpeed);
            CheckBorders();
            lastDragPosition = Input.mousePosition;
        }
    }

    private void CheckBorders()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xBorders.x, xBorders.y), 
            transform.position.y, Mathf.Clamp(transform.position.z, zBorders.x, zBorders.y));
    }

    private void UpdateZoom()
    {
        float mouseScrollWheelSpeed = Input.GetAxisRaw("Mouse ScrollWheel");
        Camera.main.orthographicSize -= mouseScrollWheelSpeed * zoomStrength;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoomBorders.x, zoomBorders.y);

        if (Camera.main.orthographicSize > zoomBorders.x && Camera.main.orthographicSize < zoomBorders.y)
            dragSpeed -= mouseScrollWheelSpeed * 0.25f;
    }

    public void AllowMovement(bool allow)
    {
        isMovingAllowed = allow;
    }
}
