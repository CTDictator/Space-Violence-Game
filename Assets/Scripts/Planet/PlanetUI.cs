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
}
