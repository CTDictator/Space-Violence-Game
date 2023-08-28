using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Decision making process for each empire.
public class EmpireAI : MonoBehaviour
{
    private EmpireProperties EP;
    [SerializeField] private List<GameObject> nearbyTargets;
    [SerializeField] private GameObject[] priorityTargets;
    [SerializeField] private List<GameObject> attackingPlanets;
    [SerializeField] private LayerMask layerMask;

    // Reference the properties to conduct actions.
    private void Start()
    {
        EP = GetComponent<EmpireProperties>();
        if (!EP.Player && !EP.Neutral)
        {
            InvokeRepeating("FindNearbyTargets", 0.0f, Constants.aiCheckFrequency);
            InvokeRepeating("SelectPriorityTargets", 0.1f, Constants.aiCheckFrequency);
            Invoke("AttackTarget", 5.0f);
        }
    }

    // Find and track nearby worlds to their own worlds.
    private void FindNearbyTargets()
    {
        nearbyTargets.Clear();
        foreach (var planet in EP.Planets)
        {
            Collider2D[] hitTargets =
                Physics2D.OverlapCircleAll((Vector2)planet.transform.position,
                Constants.planetScanRadius, layerMask);
            foreach (var hit in hitTargets)
            {
                if (hit.gameObject.GetComponent<PlanetProperties>().Empire != gameObject)
                {
                    nearbyTargets.Add(hit.gameObject);
                }
            }
        }
    }

    // Filter through all the nearby worlds and select its prefered targets.
    private void SelectPriorityTargets()
    {
        // Clean up the targets list by removing its controlled worlds from it.
        for (int i = 0; i < priorityTargets.Length; i++)
        {
            if (priorityTargets[i] != null)
            {
                // If the target is its own world, make the spot null.
                if (priorityTargets[i].GetComponent<PlanetProperties>().Empire == gameObject)
                {
                    priorityTargets[i] = null;
                }
            }
        }
        // Examine each world nearby it.
        foreach (var planet in nearbyTargets)
        {
            for (int i = 0; i < priorityTargets.Length; i++)
            {
                // If the targets spot is empty, fill it.
                if (priorityTargets[i] == null)
                {
                    priorityTargets[i] = planet;
                    break;
                }
            }
        }
    }

    // Attack one target with up to 5 worlds it has.
    private void AttackTarget()
    {
        attackingPlanets.Clear();
        int numAttackers;
        bool allOrNothing;
        // If an empire has more than 5 planets, roll how many attackers it will use.
        if (EP.Planets.Count > Constants.maxAttackers)
        {
            numAttackers = Random.Range(Constants.minAttackers, Constants.maxAttackers + 1);
            allOrNothing = false;
        }
        // Else use everything in its power.
        else
        {
            numAttackers = EP.Planets.Count;
            allOrNothing = true;
        }
        // Then select which worlds to use depending if it is small or large.
        if (allOrNothing)
        {
            foreach (var planet in EP.Planets)
            {
                attackingPlanets.Add(planet);
            }
        }
        else
        {
            for (int i = 0; i < numAttackers; i++)
            {
                int randomWorld = Random.Range(0, EP.Planets.Count);
                attackingPlanets.Add(EP.Planets[randomWorld]);
            }
        }
        // Then select a random target previously selected.
        int randomTarget = Random.Range(0, priorityTargets.Length);
        // And then attack.
        foreach (var attacker in attackingPlanets)
        {
            attacker.GetComponent<PlanetProperties>().AttackPlanet(priorityTargets[randomTarget]);
        }
        // Then repeat the process at random intervals.
        float randomTimer = Random.Range(0.5f, 1.0f);
        Invoke("AttackTarget", randomTimer);
    }
}
