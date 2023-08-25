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
    [SerializeField] private int baseCapacity;
    [SerializeField] private int modifiedCapacity;
    [Range(-1.0f, 1.0f)]
    [SerializeField] private float prosperityLevel;
    [SerializeField] private int prosperity;
    private int prosperityLimit;
    [SerializeField] private int maxCapacity;
    [SerializeField] private float shipProductionRate;
    [SerializeField] private int currentCapacity;
    private float produceAShip;
    private PlanetUI PUI;

    public string Name { get { return planetName; } }
    public PlanetType Type { get {  return planetType; } }
    public PlanetModifier[] Modifiers { get { return planetModifiers; } }
    public int CurrentCapacity { get { return currentCapacity; } }

    [Header("References:")]
    [SerializeField] private RandomNameGenerator planetNameGenerator;
    [SerializeField] private RandomPlanetTypeSelector planetTypeSelector;
    [SerializeField] private RandomPlanetModifierSelector planetModifierSelector;

    // Set up the starting values of each world.
    private void Start()
    {
        PUI = GetComponent<PlanetUI>();
        AssignPlanetName();
        planetType = planetTypeSelector.SelectRandomPlanetType();
        AssignPlanetModifiers();
        RollBaseCapacity();
        ModifyBaseCapacity();
        AssignProsperityLimit();
        AssignCurrentCapacity();
    }

    private void Update()
    {
        UpdateProsperity();
        UpdateMaxCapacity();
        UpdateShipProductionRate();
        UpdateCurrentCapacity();
    }

    // rename the world using a random name as well as the clone game object.
    private void AssignPlanetName()
    {
        planetName = planetNameGenerator.GenerateRandomPlanetName();
        gameObject.name = planetName;
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
                // If the modifier already exists or mutually exclusive with another, reroll it.
                if (modifier == newMod || modifier == newMod.MutuallyExclusiveModifier)
                    reroll = true;
            }
        } while (reroll);
        return newMod;
    }

    // Roll a base capacity for the world using the default value and a random range.
    private void RollBaseCapacity()
    {
        baseCapacity = Constants.defaultShipCapacity
            + Random.Range(-Constants.randomShipCapacityRange, 
            Constants.randomShipCapacityRange + 1);
    }

    // Modity the base value with all the planet modifiers and type.
    private void ModifyBaseCapacity()
    {
        // Base capacity + planet type capacity.
        modifiedCapacity = baseCapacity + planetType.CapacityModifier;
        foreach (PlanetModifier modifier in planetModifiers)
        {
            modifiedCapacity += modifier.CapacityModifier;
        }
        // Grant the planet the minimum capacity if it goes below minimum.
        if (modifiedCapacity < Constants.minimumCapacity)
            modifiedCapacity = Constants.minimumCapacity;
    }

    // Starting values of a planets prosperity.
    private void AssignProsperityLimit()
    {
        prosperityLimit = modifiedCapacity / 2;
        prosperity = 0;
        prosperityLevel = 0;
    }

    // Continuous update the prosperity of a world.
    private void UpdateProsperity()
    {
        if (prosperityLevel < 1.0f)
        {
            prosperityLevel += Constants.prosperityGainRate * Time.deltaTime;
            prosperity = (int)(prosperityLevel * prosperityLimit);
        }     
    }

    // Continuous update of the max capacity of the world using prosperity.
    private void UpdateMaxCapacity()
    {
        if (prosperityLevel < 1.0f) maxCapacity = modifiedCapacity + prosperity;
    }

    // Update the ship production rate based off the max capacity of a world.
    private void UpdateShipProductionRate()
    {
        if (prosperityLevel < 1.0f)
        {
            shipProductionRate = maxCapacity / Constants.timeToFullProduction;
        }
    }

    // Assign a starting capacity between half the minimum and the base capacity.
    private void AssignCurrentCapacity()
    {
        currentCapacity = Random.Range(Constants.minimumCapacity / 2, modifiedCapacity + 1);
    }

    // Develop more ships until the worlds reached the maximum capacity.
    private void UpdateCurrentCapacity()
    {
        if (currentCapacity < maxCapacity)
        {
            ProduceShips();
        }
    }

    // Use a timer to produce ships using the ship production rate.
    private void ProduceShips()
    {
        produceAShip += Time.deltaTime * shipProductionRate;
        // A new ship is made when the timer reaches 1 and don't exceed capacity.
        if (produceAShip >= 1.0f)
        {
            currentCapacity++;
            PUI.UpdateOnMapCapacityText();
            produceAShip = 0.0f;
        }
    }
}
