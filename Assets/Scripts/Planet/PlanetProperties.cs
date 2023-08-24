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
    [SerializeField] private PlanetModifier[] planetModifiers;

    public string Name { get { return planetName; } }
    public PlanetType Type { get {  return planetType; } }
    public PlanetModifier[] Modifiers { get { return planetModifiers; } }

    [Header("References:")]
    [SerializeField] private RandomNameGenerator planetNameGenerator;
    [SerializeField] private RandomPlanetTypeSelector planetTypeSelector;
    [SerializeField] private RandomPlanetModifierSelector planetModifierSelector;

    // Set up the starting values of each world.
    private void Start()
    {
        planetName = planetNameGenerator.GenerateRandomPlanetName();
        planetType = planetTypeSelector.SelectRandomPlanetType();
        AssignPlanetModifiers();
    }

    // Created a range of traits that do not conflict with each other.
    private void AssignPlanetModifiers()
    {
        // Create how many traits the planet has.
        int numModifiers = Random.Range(Constants.minAllowedModifiers, 
            Constants.maxAllowedModifiers + 1);
        planetModifiers = new PlanetModifier[numModifiers];
        for (int i = 0; i < planetModifiers.Length; i++)
        {
            planetModifiers[i] = ProduceUniqueModifier();
        }
    }

    // Ensure a unique modifier by testing against all previous modifiers.
    private PlanetModifier ProduceUniqueModifier()
    {
        PlanetModifier newMod;
        bool reroll;
        do
        {
            newMod = planetModifierSelector.SelectRandomPlanetModifier();
            reroll = false;
            foreach (PlanetModifier modifier in planetModifiers)
            {
                if (modifier == newMod) reroll = true;
            }
        } while (reroll);
        return newMod;
    }
}
