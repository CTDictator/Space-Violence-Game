using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerResize : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;

    // Reference the polygon collider component.
    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        ResizeCameraBounds();
    }

    // Resize the camera boundaries based on the map dimensions.
    public void ResizeCameraBounds()
    {
        polygonCollider.points = new[] 
        {
            new Vector2(-Constants.camRangeSize, -Constants.camRangeSize),
            new Vector2(Constants.camRangeSize, -Constants.camRangeSize),
            new Vector2(Constants.camRangeSize, Constants.camRangeSize),
            new Vector2(-Constants.camRangeSize, Constants.camRangeSize)
        };
        polygonCollider.SetPath(0, new[]
        {
            new Vector2(-Constants.camRangeSize, -Constants.camRangeSize),
            new Vector2(Constants.camRangeSize, -Constants.camRangeSize),
            new Vector2(Constants.camRangeSize, Constants.camRangeSize),
            new Vector2(-Constants.camRangeSize, Constants.camRangeSize)
        });
    }
}