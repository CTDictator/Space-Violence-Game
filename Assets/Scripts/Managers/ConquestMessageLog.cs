using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Print a message whenever a planet is conquered by the player.
[CreateAssetMenu(menuName = "ScriptableObjects/ConquestMessageLog")]
public class ConquestMessageLog : ScriptableObject
{
    [Header("# replaced by insult, @ replaced by planet.")]
    [TextArea]
    [SerializeField] private List<string> conqueredAPlanetString;
    [Header("References:")]
    [SerializeField] InsultGenerator insultGenerator;

    public string PrintConquestOf(GameObject planet)
    {
        int index = Random.Range(0, conqueredAPlanetString.Count);
        string conquest = conqueredAPlanetString[index];
        string insult = insultGenerator.GenerateInsult();
        string planetName = planet.GetComponent<PlanetProperties>().Name;
        conquest = conquest.Replace("#", insult);
        conquest = conquest.Replace("@", planetName);
        return conquest;
    }

    public string PrintDefeatOfEmpire(GameObject empire)
    {
        string empireName = empire.GetComponent<EmpireProperties>().Name;
        return $"The {empireName} has been defeated.";
    }
}
