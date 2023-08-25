using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Handles all player inputs with the mouse to play the game.
public class PlanetSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> planets;
    private Vector3 p1, p2;
    private bool dragSelect;

    private void Update()
    {
        SelectPlanets();
    }

    // Manage how a player clicks the screen to target select.
    private void SelectPlanets()
    {
        MouseClickedDown();
        MouseClickedDrag();
        MouseClickedUp();
    }

    // Actions performed on mouse down.
    private void MouseClickedDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            planets.Clear();
            dragSelect = false;
            p1 = Input.mousePosition;
        }
    }

    // Actions on mouse drag.
    private void MouseClickedDrag()
    {
        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > Constants.minDragDistance)
            {
                dragSelect = true;
            }
        }
    }

    // Actions on mouse up.
    private void MouseClickedUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!dragSelect) NonDragSelection();
            else DragSelection();
        }
    }

    // The mouse click is not a drag. so just select the planet.
    private void NonDragSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,
            0.0f, LayerMask.NameToLayer("Confiner"));
        if (hit.collider != null)
        {
            planets.Add(hit.collider.gameObject);
        }
        else
        {
            planets.Clear();
        }
    }

    // The mouse click is dragging, create a collider and select all worlds within it.
    private void DragSelection()
    {
        //-->
    }

    /*
    // Select planets to interact with.
    private void SelectPlanets()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,
                0.0f, LayerMask.NameToLayer("Confiner"));
            if (hit.collider != null)
            {
                UnselectPreviousPlanet();
                selectedPlanet = hit.collider.gameObject;
                selectedPlanet.GetComponent<PlanetUIInfo>().HighlightPlanet();
            }
            else
            {
                UnselectPreviousPlanet();
            }
        }
    }

    // Unselect the previous planet selected if any.
    private void UnselectPreviousPlanet()
    {
        if (selectedPlanet != null)
        {
            selectedPlanet.GetComponent<PlanetUIInfo>().DehighlightPlanet();
            selectedPlanet = null;
        }
    }
    */
}
