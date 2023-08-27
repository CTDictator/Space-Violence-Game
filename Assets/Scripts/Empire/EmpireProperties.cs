using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collections of all the empires properties.
public class EmpireProperties : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private string empireName;
    [SerializeField] private EmpireColour empireColor;
    [SerializeField] private List<GameObject> controlledPlanets;
    [SerializeField] private bool isNeutral;
    [SerializeField] private bool isPlayer;

    [Header("References:")]
    [SerializeField] private RandomEmpireColourSelector empireColourSelector;
    [SerializeField] private EmpireNameGenerator empireNameGenerator;

    public EmpireColour Colour { get { return empireColor; } }
    public string Name { get { return empireName; } }
    public bool Neutral { get { return isNeutral; } }
    public bool Player { get { return isPlayer; } }

    // Setup for starting values of each empire.
    private void Awake()
    {
        if (!isNeutral && !isPlayer) RollEmpireColour();
    }

    // Select a new colour and rename it.
    public void RollEmpireColour()
    {
        empireColor = empireColourSelector.GetRandomEmpireColour();
        empireName = empireNameGenerator.GenerateRandomEmpireName(empireColor.Name);
        gameObject.name = empireName;
    }

    // Assign a planet under their control.
    public void ControlPlanet(GameObject planet)
    {
        controlledPlanets.Add(planet);
        if (!isNeutral) planet.GetComponent<PlanetProperties>().ChangeEmpireOwner(gameObject);
    }

    // Unassign a planet from their control.
    public void LosePlanetControl(GameObject planet)
    {
        controlledPlanets.Remove(planet);
    }
}
