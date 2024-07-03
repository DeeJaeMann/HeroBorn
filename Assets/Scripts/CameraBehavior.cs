using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // 1
    // Distance between Main Camera and Player
    // Above and slightly behind player
    public Vector3 camOffset = new(0f, 1.2f, -2.6f);

    // 2
    // For Player Transform information
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        // 3
        // Locate Player object and retrieve it's Transform property
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 4
    // Player movement is performed in Update()
    // We want the camera movement to happen after that
    // LateUpdate() executes after Update()
    private void LateUpdate()
    {
        // 5
        // Set camera position every frame
        // TransformPoint calculates and returns a relative position in the world space
        this.transform.position = _target.TransformPoint(camOffset);

        // 6
        // Updates cameras rotation every frame focusing on Transform parameter
        this.transform.LookAt(_target);
    }
}
