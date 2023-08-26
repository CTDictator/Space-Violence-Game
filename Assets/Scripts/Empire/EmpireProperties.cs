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

    [Header("References:")]
    [SerializeField] private RandomEmpireColourSelector empireColourSelector;
    [SerializeField] private EmpireNameGenerator empireNameGenerator;

    public EmpireColour Colour { get { return empireColor; } }
    public string Name { get { return empireName; } }

    // Setup for starting values of each empire.
    private void Awake()
    {
        if (!isNeutral) RollEmpireColour();
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
    }
}
