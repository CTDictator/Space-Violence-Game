using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls to select and send fleets of ships.
public class PlanetSelector : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public GameObject selectedPlanet;
    private PlanetOverviewUI planetUI;

    private void Start()
    {
        planetUI = GetComponent<PlanetOverviewUI>();
    }

    private void Update()
    {
        SelectPlanet();
    }

    // Select a planet object to interact with.
    private void SelectPlanet()
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
                planetUI.UpdatePlanetUIInfo();
                planetUI.ShowUI();
            }
            else
            {
                UnselectPreviousPlanet();
                planetUI.HideUI();
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
}
