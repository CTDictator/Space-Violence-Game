using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Instantiate a defined set of planets into a limited area of space.
public class MapSetupManager : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private Transform container;
    private Vector3 randomLocation;

    public List<GameObject> planets = new List<GameObject>();

    // Generate worlds on start.
    private void Start()
    {
        GenerateAllPlanets();
    }

    // Generate the total number of planets within the maps size.
    private void GenerateAllPlanets()
    {
        for (int i = 0; i < Constants.numPlanets; i++)
        {
            StartCoroutine(NewPlanetLocation());
        }
    }

    // Find a unique location with no planets nearby and then instantiate a world there.
    IEnumerator NewPlanetLocation()
    {
        do
        {
            yield return null;
            randomLocation = NewRandomLocation();
        } while (Physics2D.OverlapCircle(randomLocation, Constants.minPlanetDistance, 
            LayerMask.NameToLayer("Confiner")));
        // Add the new planet to the list of worlds.
        CreateNewWorld();
        yield return null;
    }

    // Create a random location on the XY plane within the game boundaries.
    private Vector3 NewRandomLocation()
    {
        return new(Random.Range(-Constants.mapRangeSize, Constants.mapRangeSize),
                Random.Range(-Constants.mapRangeSize, Constants.mapRangeSize), 0.0f);
    }

    // Create the new planet and add it to the list for referencing.
    private void CreateNewWorld()
    {
        GameObject newPlanet = Instantiate(planet, randomLocation, Quaternion.identity, container);
        planets.Add(newPlanet);
    }
}