using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A colour and name associated with it.
[CreateAssetMenu(menuName = "EmpireColour")]
public class EmpireColour : ScriptableObject
{
    [SerializeField] private Color colour;
    [SerializeField] private string colourName;

    public Color Colour { get { return colour; } }
    public string ColourName { get { return colourName; } }
}
