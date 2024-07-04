using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    /*
     * This snippet utilizes manual Transform manipulation for basic movement
     * This does not handle the game physics as smoothly
     * The pattern that is actively applied below utilizes the physics engine to control movement
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
    */

    /*
     * Physics Engine control method
     */
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    private float _vInput;
    private float _hInput;
    // 1
    // Reference to player Rigidbody component
    private Rigidbody _rigidbody;

    // Applied jump force
    public float jumpVelocity = 5f;
    private bool _isJumping;

    // 2
    // Called before first frame update
    private void Start()
    {
        // 3
        // Checks whether the Rigidbody component exists on the GameObject
        // There is no need for error checking as the component is attached to the player object
        _rigidbody = GetComponent<Rigidbody>();
    }

    // 4
    // Transform and Rotate methods were removed
    private void Update()
    {
        _vInput = Input.GetAxis("Vertical") * moveSpeed;
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        // Returns the value if the 'J' key is pressed
        // Only fires once even if held down
        // |= : logical or condition
        // Ensures we don't have consecutive input checks overrid each other when the player is jumping
        // This is performed in the Update() rather than the FixedUpdate() method to prevent input loss or double inputs
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
    }

    // 5
    // Any physics or Rigidbody related code always goes inside the FixedUpdate method
    // FixedUpdate is frame rate independent and is used for all physics code
    private void FixedUpdate()
    {
        // 6
        // Stores left and right rotations
        Vector3 rotation = Vector3.up * _hInput;
        // 7
        // Quaternion.Euler returns a rotation value in Euler angles
        // This is a conversion to the rotation type that Unity prefers
        // Time.fixedDeltaTime is used for the same reason as Time.deltaTime in the previous snippet
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        // 8
        // Calls MovePostion which takes in Vector3 params and applies force
        // Vector break down: 
        //   Player Transform position in forward direction
        //   Multiply by the vertical inputs and Time.fixedDeltaTime
        // Rigidbody component handles applying movement force
        _rigidbody.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        // 9
        // Calls MoveRotation with takes in Vector3params and applies force
        // angleRot has horizontal inputs from keyboard
        // multiply current rotation by angleRot to get left and right rotation
        _rigidbody.MoveRotation(_rigidbody.rotation * angleRot);

        // NOTE: MovePosition and MoveRotation work differently on non-kinematic GameObjects

        if (_isJumping)
        {
            // Passing Vector3 and ForceMode params to RigidBody.AddForce makes the player jump
            // The ForceMode param determines how the force is applied. It is an Enum type
            // Impulse applies instant force to an object while taking it's mass into account
            _rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        _isJumping = false;
    }
}
