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

    public int sensitivity = 5;
    // Use this for initialization
    void Start () {
        ground = LayerMask.GetMask("Ground");
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        rig.useGravity = false;
        playerHeight = GetComponent<Collider>().bounds.size.y;
        Debug.Log(playerHeight);
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
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public bool isGrounded()
    {
        return Physics.Raycast(rig.transform.position, -Vector3.up, playerHeight/2, ground);
    }

}



/*
 
 public int speed;
    public float dist_to_ground = 1.0f;
    public float max_force = 15f;
    public float drag;

    private Rigidbody rig;
    private Vector3 rotations = Vector3.zero;
    private Vector3 impulse_force = Vector3.zero;
    public Vector3 moveDirection = Vector3.zero;
     
    /*.................................................
      IMPULSE BASED X,Y MOVEMENT
      FOR RIGIDBODY
      NO JUMP AS OF YET
      .................................................
    

 Update is called once per frame
 void FixedUpdate () {


     moveDirection.x = (Input.GetAxis("Horizontal"));
     moveDirection.z = (Input.GetAxis("Vertical"));

     impulse_force = Vector3.ClampMagnitude((speed * moveDirection * rig.mass), max_force) * Time.deltaTime;

     transform.Rotate(0.0f, Input.GetAxis("Horizontal"), 0.0f);

     if (isGrounded())
     {
         rig.AddForce(speed * impulse_force * rig.mass);
     }
 }

 public bool isGrounded()
 {
     return Physics.Raycast(rig.transform.position, -Vector3.up, dist_to_ground);
 }

*/
