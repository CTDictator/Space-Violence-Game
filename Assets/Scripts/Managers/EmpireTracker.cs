using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track of all empires in play.
public class EmpireTracker : MonoBehaviour
{
    [SerializeField] private List<GameObject> empires;

    public List<GameObject> Empires { get { return empires; } }
    // Add a newly generated planet to the list.
    public void AddEmpire(GameObject empire)
    {
        empires.Add(empire);
    }
}
