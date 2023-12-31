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
    [SerializeField] GameObject empire;
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
    public GameObject Empire { get { return empire; } }
    public int CurrentCapacity { get { return currentCapacity; } }
    public int MaxCapacity { get { return maxCapacity; } }
    public float ShipProductionRate { get {  return shipProductionRate; } }
    public int Prosperity { get { return prosperity; } }

    [Header("References:")]
    [SerializeField] private RandomNameGenerator planetNameGenerator;
    [SerializeField] private RandomPlanetTypeSelector planetTypeSelector;
    [SerializeField] private RandomPlanetModifierSelector planetModifierSelector;
    [SerializeField] private ConquestMessageLog conquestMessageLog;
    [SerializeField] private GameObject ship;
    private Transform shipContainer;
    private GameObject gameManager;

    // Set up the starting values of each world.
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        shipContainer = GameObject.Find("Ships").transform;
        PUI = GetComponent<PlanetUI>();
        AssignPlanetName();
        AssignPlanetType();
        AssignPlanetModifiers();
        AssignNeutrality();
        RollBaseCapacity();
        ModifyBaseCapacity();
        AssignProsperityLimit();
        AssignCurrentCapacity();
    }

    // Change the values of the max capacity and the current capacity.
    private void Update()
    {
        if (!empire.GetComponent<EmpireProperties>().Neutral)
        {
            UpdateProsperity();
            UpdateMaxCapacity();
            UpdateShipProductionRate();
            UpdateCurrentCapacity();
        }
        PUI.UpdateOnMapCapacityText();
    }

    // rename the world using a random name as well as the clone game object.
    private void AssignPlanetName()
    {
        planetName = planetNameGenerator.GenerateRandomPlanetName();
        gameObject.name = planetName;
    }

    private void AssignPlanetType()
    {
        planetType = planetTypeSelector.SelectRandomPlanetType();
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
            produceAShip = 0.0f;
        }
    }

    // Assign the world to a fake empire called 'Neutral'.
    private void AssignNeutrality()
    {
        empire = GameObject.Find("Neutral");
        empire.GetComponent<EmpireProperties>().ControlPlanet(gameObject);
        StartCoroutine(ChangeColour());
    }

    // Change the colour of the planet.
    private IEnumerator ChangeColour()
    {
        yield return new WaitForEndOfFrame();
        PUI.ChangeEmpireBorderColour();
    }

    // Change empire ownership.
    public void ChangeEmpireOwner(GameObject newEmpire)
    {
        empire.GetComponent<EmpireProperties>().LosePlanetControl(gameObject);
        empire = newEmpire;
        StartCoroutine(ChangeColour());
        if (empire != null && empire.GetComponent<EmpireProperties>().Player)
        {
            conquestMessageLog.PrintConquestOf(gameObject);
            gameManager.GetComponent<ConquestTextLogger>().UpdateConquestLog();

        }
    }

    // Send a fleet over to the target planet.
    public void AttackPlanet(GameObject targetPlanet)
    {
        // Reduce the current capacity by half.
        int fleetSize = currentCapacity / 2;
        currentCapacity -= fleetSize;
        StartCoroutine(SpawnShips(fleetSize, targetPlanet));
    }

    // Spawn a fleet of ships with a tiny delay between each ship.
    private IEnumerator SpawnShips(int fleetSize, GameObject targetPlanet)
    {
        for (int i = 0; i < fleetSize; i++)
        {
            yield return new WaitForNextFrameUnit();
            var newShip = Instantiate(ship, transform.position,
                Quaternion.identity, shipContainer);
            newShip.GetComponent<ShipProperties>().AcquireTarget(targetPlanet);
            newShip.GetComponent<ShipProperties>().SetEmpireOwner(empire);
        }
        yield return null;
    }

    // Detect collision onto its target planet.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var shipProperties = collision.gameObject.GetComponent<ShipProperties>();
        // Confirm the collision is its intended target.
        if (shipProperties != null && shipProperties.Target == gameObject)
        {
            if (shipProperties.Empire != empire)
            {
                AssaultPlanet(shipProperties);
            }
            else
            {
                ReinforcePlanet(shipProperties);
            }
            Destroy(collision.gameObject);
        }
    }

    // Destroy the planets defenses if it is a hostile world.
    private void AssaultPlanet(ShipProperties shipProperties)
    {
        // Trade 1 for 1 damage.
        currentCapacity -= Constants.shipStrength;
        if (prosperity > -prosperityLimit) prosperity -= Constants.shipStrength;
        prosperityLevel = (float)prosperity/prosperityLimit;
        // Check if the defenses of the world have been overwhelmed.
        if (currentCapacity < 1)
        {
            shipProperties.Empire.GetComponent<EmpireProperties>().ControlPlanet(gameObject);
        }
    }

    // Reinforce the planet if it is a friendly world.
    private void ReinforcePlanet(ShipProperties shipProperties)
    {
        // Add 1 to the defenses unless it exceeds the games maximum capacity.
        if (currentCapacity < maxCapacity) currentCapacity += Constants.shipStrength;
    }
}
