using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a random name for a planet to hold.
[CreateAssetMenu(menuName = "ScriptableObjects/RandomNameGenerator")]
public class RandomNameGenerator : ScriptableObject
{
    [SerializeField] private string[] syllable;

    // Generate a random planet name.
    public string GenerateRandomPlanetName()
    {
        // Select a number of syllables.
        int numSyllables = Random.Range(Constants.minSyllables, Constants.maxSyllables + 1);
        string planetName = string.Empty;
        for (int i = 0; i < numSyllables; i++)
        {
            int randomSyllable = Random.Range(0, syllable.Length);
            planetName += syllable[randomSyllable];
        }
        return planetName;
    }
}
