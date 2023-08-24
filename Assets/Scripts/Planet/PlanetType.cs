using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds all the information about a planet type.
[CreateAssetMenu(menuName = "PlanetType")]
public class PlanetType : ScriptableObject
{
    [SerializeField] private string planetType;
    [TextArea]
    [SerializeField] private string planetDescription;
    [SerializeField] private int planetCapacityModifier;

    public string Type { get { return planetType; } }
    public string Description { get {  return planetDescription; } }
    public int CapacityModifier { get {  return planetCapacityModifier; } }
}
