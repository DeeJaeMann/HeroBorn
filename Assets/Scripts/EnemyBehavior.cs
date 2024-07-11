using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;

    private void Start()
    {
        InitializePatrolRoute();
    }

    // OnTriggerEnter() is called whenever an object enters this object's Sphere Collider radius
    // It stores a reference to the object's Collider component
    void OnTriggerEnter(Collider other)
    {

        // We check if the Collider object is the player object
        if ( other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }
    }


    // OnTriggerExit() is called whenever an object leaves this object's Sphere Collider radius
    void OnTriggerExit(Collider other)
    {

        // We check if the Collider object is the player object
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    void InitializePatrolRoute()
    {
        // Populate locations list with Transform objects
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }
}
