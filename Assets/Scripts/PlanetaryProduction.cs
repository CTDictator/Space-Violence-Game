using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages a planets production of spaceships.
public class PlanetaryProduction : MonoBehaviour
{
    [SerializeField] private int maxShipCapacity;
    [SerializeField] private int totalShips;
    [SerializeField] private float baseProductionRate;

    private PlanetaryInfo planetaryInfo;
    private float produceAShip;

    public int TotalShips
    {
        get { return totalShips; }
    }

    // Reference the planetary production script.
    private void Start()
    {
        planetaryInfo = GetComponent<PlanetaryInfo>();
    }

    // Produce ships in a logarithmic scale, slowing as the total ships approaches max capacity.
    private void Update()
    {
        ProduceShip();
    }

    // Produce a ship based on its production rate.
    private void ProduceShip()
    {
        produceAShip += Time.deltaTime * baseProductionRate;
        // A new ship is made when the timer reaches 1 and don't exceed capacity.
        if (produceAShip >= 1.0f && totalShips < maxShipCapacity)
        {
            totalShips++;
            produceAShip = 0.0f;
            // Update the UI counter on the planet.
            planetaryInfo.UpdateTotalShipsText();
        }
    }
}
