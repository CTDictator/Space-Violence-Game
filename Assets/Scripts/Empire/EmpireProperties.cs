using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collections of all the empires properties.
public class EmpireProperties : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private string empireName;
    [SerializeField] private EmpireColour empireColor;
    [SerializeField] private GameObject[] controlledPlanets;

    [Header("References:")]
    [SerializeField] private RandomEmpireColourSelector empireColourSelector;
    [SerializeField] private EmpireNameGenerator empireNameGenerator;

    // Setup for starting values of each empire.
    private void Start()
    {
        empireColor = empireColourSelector.GetRandomEmpireColour();
        empireName = empireNameGenerator.GenerateRandomEmpireName(empireColor.Name);
    }
}
