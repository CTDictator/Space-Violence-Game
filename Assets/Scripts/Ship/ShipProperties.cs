using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Information carried by a single ship.
public class ShipProperties : MonoBehaviour
{
    [SerializeField] private GameObject empireOwner;
    [SerializeField] private GameObject target;

    private void Update()
    {
        MoveToTarget();
    }

    // Move the ship towards its target.
    private void MoveToTarget()
    {
        if (target != null)
        {
            // Face the target.
            transform.up = target.transform.position - transform.position;
            // Move towards the target.
            transform.Translate(Constants.shipSpeed * Time.deltaTime * Vector3.up);
        }
    }
}
