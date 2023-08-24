using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

// Collections of all the planets properties.
public class PlanetProperties : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private string planetName;

    public string PlanetName { get { return planetName; } }

    [Header("References:")]
    [SerializeField] private RandomNameGenerator planetNameGenerator;

    // Set up the starting values of each world.
    private void Start()
    {
        planetName = planetNameGenerator.GenerateRandomPlanetName();
    }
}
