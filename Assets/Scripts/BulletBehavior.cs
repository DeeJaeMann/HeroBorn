using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    // How long the prefab will remain in the scene after it is instantiated
    public float onscreenDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        // Delete the game object on the specified delay
        Destroy(this.gameObject, onscreenDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
