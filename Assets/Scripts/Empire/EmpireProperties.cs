using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collections of all the empires properties.
public class EmpireProperties : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private EmpireColour empireColour;

    [Header("References:")]
    [SerializeField] private RandomEmpireColourSelector empireColourSelector;

    // Setup for starting values of each empire.
    private void Start()
    {
        empireColour = empireColourSelector.GetRandomEmpireColour();
    }
}
