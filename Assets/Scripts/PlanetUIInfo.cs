using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle all the user interface of each world.
public class PlanetUIInfo : MonoBehaviour
{
    [SerializeField] private GameObject selectionRing;

    // Highlight the selected planet.
    public void HighlightPlanet()
    {
        selectionRing.SetActive(true);
    }

    // Unhighlight the planet.
    public void DehighlightPlanet()
    {
        selectionRing.SetActive(false);
    }
}
