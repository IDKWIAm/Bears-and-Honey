using UnityEngine;

public class DraggableCamera : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 0.45f;

    [SerializeField] Vector2 xBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);
    [SerializeField] Vector2 zBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    [SerializeField] float zoomStrength = 1f;

    [SerializeField] Vector2 zoomBorders = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    [SerializeField] GameObject bearsList;

    private Vector3 lastDragPosition;

    private Camera mainCamera;

    public bool isMovingAllowed { get; private set; } = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isMovingAllowed || !mainCamera.gameObject.activeSelf) return;
        if (bearsList != null && bearsList.activeSelf == true) return;

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
        mainCamera.orthographicSize -= mouseScrollWheelSpeed * zoomStrength;

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, zoomBorders.x, zoomBorders.y);

        if (mainCamera.orthographicSize > zoomBorders.x && mainCamera.orthographicSize < zoomBorders.y)
            dragSpeed -= mouseScrollWheelSpeed * 0.25f;
    }

    public void AllowMovement(bool allow)
    {
        isMovingAllowed = allow;
    }
}
