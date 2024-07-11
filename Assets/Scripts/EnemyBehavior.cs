using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Allows access to Unity's navigation classes

public class EnemyBehavior : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;

    // Current patrol point destination
    private int _locationIndex = 0;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();

        MoveToNextPatrolLocation();
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

    void MoveToNextPatrolLocation()
    {
        // Sets destination
        // destination is a Vector3 position in 3D space
        // .position references the list element Vector3 position (patrol point)
        _agent.destination = locations[_locationIndex].position;
    }
}
