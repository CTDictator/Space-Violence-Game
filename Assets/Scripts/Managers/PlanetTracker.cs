using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks all the planets in game.
public class PlanetTracker : MonoBehaviour
{
    [SerializeField] private List<GameObject> planets;

    public List<GameObject> Planets { get { return planets; } }

    // Add a generated planet to the list.
    public void AddPlanet(GameObject planet)
    {
        planets.Add(planet);
    }
}
