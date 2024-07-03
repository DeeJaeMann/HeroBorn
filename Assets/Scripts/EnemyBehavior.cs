using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 1
    // OnTriggerEnter() is called whenever an object enters this object's Sphere Collider radius
    // It stores a reference to the object's Collider component
    void OnTriggerEnter(Collider other)
    {
        // 2
        // We check if the Collider object is the player object
        if ( other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }
    }

    // 3
    // OnTriggerExit() is called whenever an object leaves this object's Sphere Collider radius
    void OnTriggerExit(Collider other)
    {
        // 4
        // We check if the Collider object is the player object
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
