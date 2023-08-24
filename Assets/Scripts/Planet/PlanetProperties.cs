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
    [SerializeField] private PlanetType planetType;

    public string PlanetName { get { return planetName; } }
    public PlanetType PlanetType { get {  return planetType; } }

    [Header("References:")]
    [SerializeField] private RandomNameGenerator planetNameGenerator;
    [SerializeField] private RandomPlanetTypeSelector planetTypeSelector;

    // Set up the starting values of each world.
    private void Start()
    {
        planetName = planetNameGenerator.GenerateRandomPlanetName();
        planetType = planetTypeSelector.SelectRandomPlanetType();
    }
}
