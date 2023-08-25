using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hold an array of all colours available.
[CreateAssetMenu(menuName = "ScriptableObjects/RandomEmpireColourSelector")]
public class RandomEmpireColourSelector : ScriptableObject
{
    [SerializeField] private EmpireColour[] empireColour;

    // Return a colour at random to the caller.
    public EmpireColour GetRandomEmpireColour()
    {
        int index = Random.Range(0, empireColour.Length);
        return empireColour[index];
    }
}
