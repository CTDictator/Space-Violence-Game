using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Information carried by a single ship.
public class ShipProperties : MonoBehaviour
{
    [SerializeField] private GameObject empireOwner;
    [SerializeField] private GameObject target;

    public GameObject Empire { get { return empireOwner; } }
    public GameObject Target { get { return target; } }

    // Remove the ship if it has no target.
    private void Start()
    {
        if (target == null) Destroy(gameObject);
    }

    private void FixedUpdate()
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

    // Change the target of the ship.
    public void AcquireTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    // Set the empire affiliation of the ship.
    public void SetEmpireOwner(GameObject newEmpire)
    {
        empireOwner = newEmpire;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = empireOwner.GetComponent<EmpireProperties>().Colour.Colour;
    }
}
