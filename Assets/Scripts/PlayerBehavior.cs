using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // 1
    // Speed player moves forward or backward
    public float moveSpeed = 10f;
    // Speed player rotates left or right
    public float rotateSpeed = 75f;

    // 2
    // Vertical axis input
    private float _vInput;
    // Horizontal axis input
    private float _hInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 3
        // Detects when up arrow, down arrow, W or S key is pressed
        // Up/W returns 1
        // Down/S returns -1
        _vInput = Input.GetAxis("Vertical") * moveSpeed;

        // 4
        // Detects when left arrow, right arrow, A or D key is pressed
        // Right/D returns 1
        // Left/A returns -1
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        // 5
        // Direction and speed player needs to move forward/back along z axis
        this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);

        // 6
        // Left/Right rotation  
        this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }
}
