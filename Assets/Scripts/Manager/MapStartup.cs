using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn in the planets and empires at the start of the game.
public class MapStartup : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private GameObject empire;
    [SerializeField] private GameObject planet;
    [SerializeField] private Transform empireContainer;
    [SerializeField] private Transform planetContainer;
    private Vector3 randomLocation;

    // Generate worlds on start.
    private void Start()
    {
        GenerateAllEmpires();
        GenerateAllPlanets();
    }

    // Generate all the empires within the game.
    private void GenerateAllEmpires()
    {
        for (int i = 0; i < Constants.numEmpires;  i++)
        {
            Instantiate(empire, Vector3.zero, Quaternion.identity, empireContainer);
        }
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
        Instantiate(planet, randomLocation, Quaternion.identity, planetContainer);
    }
}
