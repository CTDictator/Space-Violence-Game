using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generate a random insult combining an adjective and a noun.
[CreateAssetMenu(menuName = "ScriptableObjects/InsultGenerator")]
public class InsultGenerator : ScriptableObject
{
    [SerializeField] private List<string> adjective;
    [SerializeField] private List<string> noun;

    // Take two random words and combine them to form the insult.
    public string GenerateInsult()
    {
        string insult;
        int index = Random.Range(0, adjective.Count);
        insult = $"{adjective[index]} ";
        index = Random.Range(0, noun.Count);
        insult += noun[index];
        return insult;
    }
}
