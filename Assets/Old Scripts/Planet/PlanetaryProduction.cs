using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages a planets production of spaceships.
public class PlanetaryProduction : MonoBehaviour
{
    [SerializeField] private float productionRate;
    [SerializeField] private int maxShipCapacity;
    [SerializeField] private int totalShips;

    private GameObject playerEmpire;
    private PlanetaryInfo planetaryInfo;
    private float produceAShip;

    public int MaxCapacity { get { return maxShipCapacity; } }
    public int TotalShips { get { return totalShips; } }

    // Reference the planetary production script.
    private void Start()
    {
        planetaryInfo = GetComponent<PlanetaryInfo>();
        maxShipCapacity = AssignShipCapacity();
        productionRate = AssignProductionRate();
        totalShips = AssignTotalShips();
        playerEmpire = GameObject.Find("Player Empire");
    }

    // Produce ships in a logarithmic scale, slowing as the total ships approaches max capacity.
    private void Update()
    {
        ProduceShip();
    }

    // Produce a ship based on its production rate.
    private void ProduceShip()
    {
        produceAShip += Time.deltaTime * productionRate;
        // A new ship is made when the timer reaches 1 and don't exceed capacity.
        if (produceAShip >= 1.0f && totalShips < maxShipCapacity)
        {
            totalShips++;
            produceAShip = 0.0f;
            // Update the UI counter on the planet.
            planetaryInfo.UpdateTotalShipsText();
            playerEmpire.GetComponent<PlanetOverviewUI>().UpdatePlanetCapacity();
        }
    }

    // Assign the maximum ship capacity based off planet type and modifiers.
    private int AssignShipCapacity()
    {
        // Base planetary capacity.
        int capacity = Constants.defaultShipCapacity
            + Random.Range(-Constants.randomShipCapacityRange, 
            Constants.randomShipCapacityRange + 1);
        // Planet type modifier added.
        capacity += PlanetTypeCapacityModifier();
        // Planet modifier added.
        capacity += PlanetModifierCapacityModifier();
        // If a planet rolls horrendously, just give it a minimum 10 capacity.
        return (capacity >= Constants.minimumCapacity) ? capacity : Constants.minimumCapacity;
    }

    // Modify the planetary capacity using the planet type.
    private int PlanetTypeCapacityModifier()
    {
        switch (planetaryInfo.PlanetType)
        {
            case PlanetTypes.terrestrial: return Constants.greatHabitabilityScore;
            case PlanetTypes.oceanic: return Constants.normalHabitabilityScore;
            case PlanetTypes.desert: return Constants.normalHabitabilityScore;
            case PlanetTypes.rocky: return Constants.poorHabitabilityScore;
            case PlanetTypes.icey: return Constants.poorHabitabilityScore;
            case PlanetTypes.gaseous: return Constants.awfulHabitabilityScore;
            case PlanetTypes.molten: return Constants.awfulHabitabilityScore;
            default: return 0;
        }
    }

    // Modify the planetary capacity using the planets modifiers.
    private int PlanetModifierCapacityModifier()
    {
        int capacity = 0;
        foreach (PlanetModifiers mod in planetaryInfo.PlanetModifier)
        {
            switch (mod)
            {
                case PlanetModifiers.thin_atmosphere:
                case PlanetModifiers.weak_magnetic_field:
                case PlanetModifiers.low_corruption:
                case PlanetModifiers.low_polution:
                    capacity += Constants.smallPositiveMod; break;
                case PlanetModifiers.thick_atmosphere:
                case PlanetModifiers.strong_magnetic_field:
                case PlanetModifiers.high_corruption:
                case PlanetModifiers.high_polution:
                    capacity += Constants.smallNegativeMod; break;
                case PlanetModifiers.low_gravity:
                case PlanetModifiers.good_infrastructure:
                case PlanetModifiers.high_popular_support:
                case PlanetModifiers.mineral_rich:
                case PlanetModifiers.densely_populated:
                    capacity += Constants.largePositiveMod; break;
                case PlanetModifiers.high_gravity:
                case PlanetModifiers.poor_infrastructure:
                case PlanetModifiers.high_social_unrest:
                case PlanetModifiers.mineral_poor:
                case PlanetModifiers.sparcely_populated:
                    capacity += Constants.largeNegativeMod; break;
                default: break;
            }
        }
        return capacity;
    }

    // Production rate is the time it takes from empty to full in a certain time period.
    private float AssignProductionRate()
    {
        return maxShipCapacity / Constants.timeToFullProduction;
    }

    // Randomly roll the starting defenses of the planet.
    private int AssignTotalShips()
    {
        return Random.Range(Constants.minimumCapacity, maxShipCapacity + 1);
    }
}
