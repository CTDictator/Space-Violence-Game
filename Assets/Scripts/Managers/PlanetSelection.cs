using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

// Handles all player inputs with the mouse to play the game.
public class PlanetSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> planets;
    private Vector3 p1;
    private bool dragSelect;
    [SerializeField] private GameObject selectionBox;
    [SerializeField] private LayerMask layerMask;

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
        TargetSelected();
    }

    // Actions performed on mouse down.
    private void MouseClickedDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragSelect = false;
            p1 = PointOnScreen();
        }
    }

    // Actions on mouse drag.
    private void MouseClickedDrag()
    {
        if (Input.GetMouseButton(0))
        {
            if ((p1 - PointOnScreen()).magnitude > Constants.minDragDistance)
            {
                dragSelect = true;
            }
            if (dragSelect)
            {
                if (!selectionBox.activeInHierarchy) selectionBox.SetActive(true);
                Vector3 MousePos = PointOnScreen();
                selectionBox.transform.position = Vector3.Lerp(p1, MousePos, 0.5f);
                selectionBox.transform.localScale 
                    = new(Mathf.Abs(p1.x-MousePos.x), Mathf.Abs(p1.y - MousePos.y), 0.0f);
            }
        }
    }

    // Actions on mouse up.
    private void MouseClickedUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HideAllSelections();
            if (!dragSelect) NonDragSelection();
            else DragSelection();
            selectionBox.SetActive(false);
        }
    }

    // Display all the worlds selected.
    private void ShowAllSelections()
    {
        foreach (var planet in planets)
        {
            planet.GetComponent<PlanetUI>().ShowSelectionRing();
        }
    }

    // Deselect all the worlds within the array and hide their selection icon.
    private void HideAllSelections()
    {
        foreach (var planet in planets)
        {
            planet.GetComponent<PlanetUI>().HideSelectionRing();
        }
        planets.Clear();
    }

    // Convert a camera point into a point in game.
    private Vector3 PointOnScreen()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        return point;
    }

    // The mouse click is not a drag. so just select the planet.
    private void NonDragSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(p1));
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.0f, layerMask);
        // Confirm there is something that was hit.
        if (hit.collider != null)
        {
            PlanetProperties planetHit = hit.collider.gameObject.GetComponent<PlanetProperties>();
            bool isPlayer = planetHit.Empire.GetComponent<EmpireProperties>().Player;
            // Confirm it belongs to the player.
            if (isPlayer)
            {
                planets.Add(hit.collider.gameObject);
            }
            // Reveal the selection.
            ShowAllSelections();
        }
        else
        {
            // Hide all selections.
            HideAllSelections();
        }
    }

    // The mouse click is dragging, create a collider and select all worlds within it.
    private void DragSelection()
    {
        Vector2 point = selectionBox.transform.position;
        Vector2 size = selectionBox.transform.localScale;
        Collider2D[] hits = Physics2D.OverlapBoxAll(point, size, 0.0f, layerMask);
        // Confirm something was hit.
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                PlanetProperties planetHit = hit.gameObject.GetComponent<PlanetProperties>();
                bool isPlayer = planetHit.Empire.GetComponent<EmpireProperties>().Player;
                // Confirm the planet belongs to the player.
                if (isPlayer)
                {
                    planets.Add(hit.gameObject);
                }
            }
            ShowAllSelections();
        }
        else
        {
            HideAllSelections();
        }
    }

    // Confirm a target for the player to attack once they have their worlds selected.
    private void TargetSelected()
    {
        // Confirm there is at least 1 planet selected on right click.
        if (Input.GetMouseButtonDown(1) && planets.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.0f, layerMask);
            // If a target is hit, send a fleet to the target.
            if (hit.collider != null)
            {
                foreach (var planet in planets)
                {
                    planet.GetComponent<PlanetProperties>().AttackPlanet(hit.collider.gameObject);
                }
            }
        }
    }
}
