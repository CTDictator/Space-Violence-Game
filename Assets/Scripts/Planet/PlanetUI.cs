using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Displays all information necessary to the user.
public class PlanetUI : MonoBehaviour
{
    private PlanetProperties PP;

    [Header("References: ")]
    [SerializeField] private TextMeshProUGUI onMapCapacityText;
    [SerializeField] private GameObject selectionRing;
    [SerializeField] private GameObject empireBorderRing;

    // Reference setup.
    private void Start()
    {
        PP = GetComponent<PlanetProperties>();
    }

    // Change the current capacity the player sees on each world.
    public void UpdateOnMapCapacityText()
    {
        onMapCapacityText.text = $"{PP.CurrentCapacity}";
    }

    // Reveal that the planet has been selected by the player.
    public void ShowSelectionRing()
    {
        selectionRing.SetActive(true);
    }

    // Reveal that the planet has been selected by the player.
    public void HideSelectionRing()
    {
        selectionRing.SetActive(false);
    }

    // Change the colour of the border ring on the planet.
    public void ChangeEmpireBorderColour()
    {
        if (PP.Empire != null)
        {
            empireBorderRing.GetComponent<SpriteRenderer>().color
                = PP.Empire.GetComponent<EmpireProperties>().Colour.Colour;
        }
    }
}
