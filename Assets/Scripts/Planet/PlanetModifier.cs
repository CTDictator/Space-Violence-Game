using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds all the information about a planet modifier.
[CreateAssetMenu(menuName = "PlanetModifier")]
public class PlanetModifier : ScriptableObject
{
    [SerializeField] private string modifierName;
    [TextArea]
    [SerializeField] private string modifierDescription;
    [SerializeField] private int planetCapacityModifier;
    [SerializeField] private PlanetModifier mutuallyExclusiveModifier;

    public string Name { get { return modifierName; } }
    public string Description { get { return modifierDescription; } }
    public int CapacityModifier { get { return planetCapacityModifier; } }
    public PlanetModifier MutuallyExclusiveModifier { get { return mutuallyExclusiveModifier; } }
}
