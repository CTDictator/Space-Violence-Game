using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpireOwnership : MonoBehaviour
{
    [SerializeField] private MapSetupManager mapManager;
    [SerializeField] private List<GameObject> planetsOwned = new List<GameObject>();
}
