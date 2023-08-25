using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a name for the caller to use as their empire name.
[CreateAssetMenu(menuName = "ScriptableObjects/EmpireNameGenerator")]
public class EmpireNameGenerator : ScriptableObject
{
    [Header("Empire Color replaces \'#\'")]
    [SerializeField] private string[] empireName;

    // Recieve the empires color and mix it with a randomly selected name.
    public string GenerateRandomEmpireName(string color)
    {
        int index = Random.Range(0, empireName.Length);
        string newEmpireName = empireName[index];
        newEmpireName = newEmpireName.Replace("#", color);
        return newEmpireName;
    }
}
