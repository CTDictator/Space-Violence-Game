using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetOverviewUI : MonoBehaviour
{
    private PlanetSelector PS;
    [SerializeField] private GameObject planetUIInfoPanel;
    [SerializeField] private TextMeshProUGUI planetName;
    [SerializeField] private TextMeshProUGUI planetType;
    [SerializeField] private TextMeshProUGUI planetCapacity;
    [SerializeField] private TextMeshProUGUI planetProduction;
    [SerializeField] private TextMeshProUGUI[] planetTraits;
    [SerializeField] private TextMeshProUGUI[] planetTraitsInfo;

    // Reference the PlanetUIInfo script to read information about the selected planet.
    private void Start()
    {
        PS = GetComponent<PlanetSelector>();
    }

    // Other scripts can call for an update of the selected world information.
    public void UpdatePlanetUIInfo()
    {
        UpdatePlanetName();
        UpdatePlanetType();
    }

    // Updates the name of the world onto the UI.
    private void UpdatePlanetName()
    {
        planetName.text = PS.selectedPlanet.GetComponent<PlanetaryInfo>().Name;
    }

    // Hide the UI when nothing is selected.
    public void HideUI()
    {
        planetUIInfoPanel.SetActive(false);
    }

    // Show the UI when something is selected.
    public void ShowUI()
    {
        planetUIInfoPanel.SetActive(true);
    }

    // Update and display the planet type onto the ui.
    private void UpdatePlanetType()
    {
        string typeName = string.Empty;
        PlanetTypes pt = PS.selectedPlanet.GetComponent<PlanetaryInfo>().PlanetType;
        switch (pt)
        {
            case PlanetTypes.terrestrial:
                typeName = "terrestrial"; break;
            case PlanetTypes.oceanic:
                typeName = "oceanic"; break;
            case PlanetTypes.desert:
                typeName = "desert"; break;
            case PlanetTypes.rocky:
                typeName = "rocky"; break;
            case PlanetTypes.icey:
                typeName = "icey"; break;
            case PlanetTypes.gaseous:
                typeName = "gaseous"; break;
            case PlanetTypes.molten:
                typeName = "molten"; break;
            default: break;
        }
        planetType.text = typeName;
    }


}
