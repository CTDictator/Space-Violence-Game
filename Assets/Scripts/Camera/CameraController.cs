using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera mainCamera;
    private float horizontalMove;
    private float verticalMove;
    private float scrollMove;

    // Reference the camera component.
    private void Start()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();
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
            * Constants.cameraSpeed * mainCamera.m_Lens.OrthographicSize;
        verticalMove = Input.GetAxis("Vertical") * Time.deltaTime 
            * Constants.cameraSpeed * mainCamera.m_Lens.OrthographicSize;
        transform.Translate(horizontalMove, verticalMove, 0.0f);
        LockCameraBoundaries();
    }

    // Lock the camera movement within map boundaries - orthographic size.
    private void LockCameraBoundaries()
    {
        float newClamp = Constants.camRangeSize - mainCamera.m_Lens.OrthographicSize;
        float newX = Mathf.Clamp(transform.position.x, -newClamp, newClamp);
        float newY = Mathf.Clamp(transform.position.y, -newClamp, newClamp);
        transform.position = new(newX, newY, transform.position.z);
    }

    // Use the mouse wheel to zoom in and out on the map.
    private void ZoomCameraMovement()
    {
        scrollMove = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.m_Lens.OrthographicSize -= scrollMove * mainCamera.m_Lens.OrthographicSize;
        mainCamera.m_Lens.OrthographicSize = Mathf.Clamp(mainCamera.m_Lens.OrthographicSize,
        Constants.minCameraOrthoSize, Constants.maxCameraOrthoSize) ;
    }
}
