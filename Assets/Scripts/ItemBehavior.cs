using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // When an object collides with this item OnCollisionEnter() is called
    // This method has a param that stores a reference to the collider that ran into it
    private void OnCollisionEnter(Collision collision)
    {
        // gameObject holds a reference to the colliding GameObject's Collider
        // We check if that object is the player object
        if ( collision.gameObject.name == "Player")
        {
            // Call the Destroy method to remove the object from the scene
            Destroy(this.transform.gameObject);
            // Debug output that the item has been collected
            Debug.Log("Item collected!");

            gameManager.Items++;
        }
    }
}
