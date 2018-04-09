using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rig;
    public Camera main_cam;

    //PLAYER variables
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;

    private bool grounded = false;
    private float speed = 10.0f;
    private LayerMask ground;

    //CAM variables
    private float pitch;
    private float yaw;
    private float playerHeight;
    private Detection allyDetection;
    public int sensitivity = 5;

    // Use this for initialization
    void Start () {
        ground = LayerMask.GetMask("Ground");
        rig = GetComponent<Rigidbody>();
        rig.useGravity = false;
        playerHeight = GetComponent<Collider>().bounds.size.y;
    }
   

    void FixedUpdate()
    {
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rig.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rig.AddForce(velocityChange, ForceMode.VelocityChange);

        if (isGrounded())
        {
            // Jump
            if (Input.GetButton("Jump"))
            {
                rig.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rig.AddForce(new Vector3(0, -gravity * rig.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {       
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public bool isGrounded()
    {
        return Physics.Raycast(rig.transform.position, -Vector3.up, playerHeight/2, ground);
    }

}

