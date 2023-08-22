using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private float horizontalMove;
    private float verticalMove;
    private float scrollMove;

    // Reference the camera component.
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Move the camera based on the controls.
    private void Update()
    {
        // Horizontal/Vertical input movements.
        XYCameraMovement();

        // Zooming in and out using mouse wheel.
        ZoomCameraMovement();
    }

    // Use the horizontal/vertical inputs to move the camera along the XY axis.
    private void XYCameraMovement()
    {
        horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime 
            * Constants.cameraSpeed * mainCamera.orthographicSize;
        verticalMove = Input.GetAxis("Vertical") * Time.deltaTime 
            * Constants.cameraSpeed * mainCamera.orthographicSize;
        transform.Translate(horizontalMove, verticalMove, 0.0f);
    }

    // Use the mouse wheel to zoom in and out on the map.
    private void ZoomCameraMovement()
    {
        scrollMove = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize -= scrollMove * mainCamera.orthographicSize;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize,
            Constants.minCameraOrthoSize, Constants.maxCameraOrthoSize);
    }
}
