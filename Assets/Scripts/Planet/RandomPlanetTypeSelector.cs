using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Select a random planet type and return it to the caller.
[CreateAssetMenu(menuName = "ScriptableObjects/RandomPlanetTypeSelector")]
public class RandomPlanetTypeSelector : ScriptableObject
{
    [SerializeField] private PlanetType[] planetTypes;

    public PlanetType SelectRandomPlanetType()
    {
        int index = Random.Range(0, planetTypes.Length);
        return planetTypes[index];
    }
}
