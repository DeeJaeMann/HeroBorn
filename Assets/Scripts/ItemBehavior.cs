using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
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
    // When an object collides with this item OnCollisionEnter() is called
    // This method has a param that stores a reference to the collider that ran into it
    private void OnCollisionEnter(Collision collision)
    {
        // 2
        // gameObject holds a reference to the colliding GameObject's Collider
        // We check if that object is the player object
        if ( collision.gameObject.name == "Player")
        {
            // 3
            // Call the Destroy method to remove the object from the scene
            Destroy(this.transform.gameObject);
            // 4
            // Debug output that the item has been collected
            Debug.Log("Item collected!");
        }
    }
}
