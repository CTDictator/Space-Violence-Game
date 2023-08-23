using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a random name for a planet to hold.
[CreateAssetMenu(menuName = "ScriptableObjects/RandomNameGenerator")]
public class RandomNameGenerator : ScriptableObject
{
    [SerializeField] private string[] syllable;

    /*
    public string GenerateRandomPlanetName()
    {
        int syllables = Random.Ran;
        string planetName;
    }
    */
}
