using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Select a random planet modifier and return it to the caller.
[CreateAssetMenu(menuName = "ScriptableObjects/RandomPlanetModifierSelector")]
public class RandomPlanetModifierSelector : ScriptableObject
{
    [SerializeField] private PlanetModifier[] planetModifiers;

    public PlanetModifier SelectRandomPlanetModifier()
    {
        int index = Random.Range(0, planetModifiers.Length);
        return planetModifiers[index];
    }
}
