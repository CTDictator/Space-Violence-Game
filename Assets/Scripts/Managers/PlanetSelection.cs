using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject playerEmpire;
    [SerializeField] private GameObject planetInfoUI;
    [SerializeField] private TextMeshProUGUI planetNameText;
    [SerializeField] private TextMeshProUGUI planetTypeText;
    [SerializeField] private TextMeshProUGUI planetCapacityText;
    [SerializeField] private TextMeshProUGUI planetProductionText;
    [SerializeField] private TextMeshProUGUI[] planetModifierNameText;
    [SerializeField] private TextMeshProUGUI[] planetModifierDescriptionText;

    private void Update()
    {
        SelectPlanets();

    }

    // Manage how a player clicks the screen to target select.
    private void SelectPlanets()
    {
        RemoveLostPlanets();
        MouseClickedDown();
        MouseClickedDrag();
        MouseClickedUp();
        TargetSelected();
        SelectAll();
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
            RevealInfoOfPlanet(hit.collider.gameObject);
        }
        else
        {
            // Hide all selections.
            HideAllSelections();
            HideInfoOfPlanet();
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

    // Use a hotkey to select all player worlds.
    private void SelectAll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            HideAllSelections();
            var empire = playerEmpire.GetComponent<EmpireProperties>().Planets;
            foreach (var planet in empire)
            {
                planets.Add(planet);
            }
            ShowAllSelections();
        }
    }

    // If you happen to have lost a planet, unselect it.
    private void RemoveLostPlanets()
    {
        foreach(var planet in planets)
        {
            if (!planet.GetComponent<PlanetProperties>().Empire.GetComponent<EmpireProperties>().Player)
            {
                planet.GetComponent<PlanetUI>().HideSelectionRing();
                planets.Remove(planet);
            }
        }
    }

    // Show information about the planet selected.
    private void RevealInfoOfPlanet(GameObject selectedPlanet)
    {
        PlanetProperties SPP = selectedPlanet.transform.GetComponent<PlanetProperties>();
        planetNameText.text = SPP.Name;
        planetTypeText.text = SPP.Type.Type;
        planetCapacityText.text = $"{SPP.CurrentCapacity}/{SPP.MaxCapacity}";
        planetProductionText.text = $"{SPP.ShipProductionRate.ToString("f1")} Ships/sec";
        foreach (var modName in planetModifierNameText) modName.text = string.Empty;
        foreach (var modDesc in planetModifierDescriptionText) modDesc.text = string.Empty;
        for (int i = 0; i < SPP.Modifiers.Length; i++)
        {
            planetModifierNameText[i].text = SPP.Modifiers[i].Name;
            planetModifierDescriptionText[i].text = SPP.Modifiers[i].Description;
        }
        planetInfoUI.SetActive(true);
    }

    // Hide the information about a planet.
    private void HideInfoOfPlanet()
    {
        planetInfoUI.SetActive(false);
    }
}
