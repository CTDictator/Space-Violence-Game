using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to resize the mask to scale correctly when dragging.
public class SpriteMaskResizer : MonoBehaviour
{
    private const float border = 0.02f;
    [SerializeField] private GameObject selectorBox;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void LateUpdate()
    {
        if (selectorBox.activeInHierarchy)
        {
            float xPos = selectorBox.transform.localScale.x - border * cam.m_Lens.OrthographicSize;
            float yPos = selectorBox.transform.localScale.y - border * cam.m_Lens.OrthographicSize;
            gameObject.transform.localScale = new(xPos, yPos, 0.0f);
            gameObject.transform.localPosition = selectorBox.transform.localPosition;
        }
    }
}
