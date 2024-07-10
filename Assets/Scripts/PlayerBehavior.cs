using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    /*
     * This snippet utilizes manual Transform manipulation for basic movement
     * This does not handle the game physics as smoothly
     * The pattern that is actively applied below utilizes the physics engine to control movement
    
    // Speed player moves forward or backward
    public float moveSpeed = 10f;
    // Speed player rotates left or right
    public float rotateSpeed = 75f;

    
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
        
        // Detects when up arrow, down arrow, W or S key is pressed
        // Up/W returns 1
        // Down/S returns -1
        _vInput = Input.GetAxis("Vertical") * moveSpeed;

        
        // Detects when left arrow, right arrow, A or D key is pressed
        // Right/D returns 1
        // Left/A returns -1
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        
        // Direction and speed player needs to move forward/back along z axis
        this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);

        
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
    
    // Reference to player Rigidbody component
    private Rigidbody _rigidbody;

    // Applied jump force
    public float jumpVelocity = 5f;
    private bool _isJumping;

    // For checking the distance between player capsuleCollider and groundLayer objects
    public float distanceToGround = 0.1f;

    // Used for collider detection, can be set in inspector
    public LayerMask groundLayer;

    // collider component
    private CapsuleCollider _capsuleCollider;

    // stores the bullet prefab
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private bool _isShooting;

    
    // Called before first frame update
    private void Start()
    {
        
        // Checks whether the Rigidbody component exists on the GameObject
        // There is no need for error checking as the component is attached to the player object
        _rigidbody = GetComponent<Rigidbody>();

        // Gets the capsule collider attached to the player
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    
    // Transform and Rotate methods were removed
    private void Update()
    {
        _vInput = Input.GetAxis("Vertical") * moveSpeed;
        _hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        // Returns the value if the 'Space' key is pressed
        // Only fires once even if held down
        // |= : logical or condition
        // Ensures we don't have consecutive input checks overriding each other when the player is jumping
        // This is performed in the Update() rather than the FixedUpdate() method to prevent input loss or double inputs
        // the shooting logic is the same as jumping except it's using the right shift key for input
        _isJumping |= Input.GetKeyDown(KeyCode.Space);
        _isShooting |= Input.GetKeyDown(KeyCode.RightShift);
    }

    
    // Any physics or Rigidbody related code always goes inside the FixedUpdate method
    // FixedUpdate is frame rate independent and is used for all physics code
    private void FixedUpdate()
    {
        
        // Stores left and right rotations
        Vector3 rotation = Vector3.up * _hInput;
        
        // Quaternion.Euler returns a rotation value in Euler angles
        // This is a conversion to the rotation type that Unity prefers
        // Time.fixedDeltaTime is used for the same reason as Time.deltaTime in the previous snippet
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        
        // Calls MovePostion which takes in Vector3 params and applies force
        // Vector break down: 
        //   Player Transform position in forward direction
        //   Multiply by the vertical inputs and Time.fixedDeltaTime
        // Rigidbody component handles applying movement force
        _rigidbody.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        
        // Calls MoveRotation with takes in Vector3params and applies force
        // angleRot has horizontal inputs from keyboard
        // multiply current rotation by angleRot to get left and right rotation
        _rigidbody.MoveRotation(_rigidbody.rotation * angleRot);

        // NOTE: MovePosition and MoveRotation work differently on non-kinematic GameObjects

        // Checks if the player is on the ground and the jump key is pressed
        if (IsGrounded() && _isJumping)
        {
            // Passing Vector3 and ForceMode params to RigidBody.AddForce makes the player jump
            // The ForceMode param determines how the force is applied. It is an Enum type
            // Impulse applies instant force to an object while taking it's mass into account
            _rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        // Reset jumping state
        _isJumping = false;

        // A new local GameObject variable is created every time the right shift key is spressed
        // Instatiate is used to assign a game object to newBullet by passing in the bullet prefab. The player position is used
        // to place the new bullet prefab in front of the player
        // This is appended as a GameObject to explicitly cast the return object as the same type as newBullet (GameObject in this case)
        if (_isShooting)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation);

            // Stores a Rigidbody component on newBullet
            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();

            // Set the velocity to the player's transform forward direction multiplied by bulletSpeed
            bulletRigidbody.velocity = this.transform.forward * bulletSpeed;
        }

        // Reset shooting state
        _isShooting = false;

    }

    private bool IsGrounded()
    {
        // Stores the position at the bottom of the player's capsule Collider to check for collisions with any objects on the ground layer
        // All collider components have a bounds property which has min, max and center positions of its x, y and z axes
        // The bottom of the collider is the 3D point at center x, min y and center z
        Vector3 capsuleBottom = new Vector3(_capsuleCollider.bounds.center.x, _capsuleCollider.bounds.min.y, _capsuleCollider.bounds.center.z);

        // Stores the result of check capsule method from the Physics class
        // Takes 5 arguments: 
        //  start of the capsule - set to middle of the capsule collider because we're only checking if the bottom touches the ground
        //  end of the capsule - set to capsuleBottom (already calculated)
        //  radius of the capsule - distanceToGround (already set)
        //  layer mask to check for collisions - groundLayer
        //  query trigger interaction - determins whether the medthod should ignore colliders that are set as triggers
        bool grounded = Physics.CheckCapsule(_capsuleCollider.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
