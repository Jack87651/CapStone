using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    WheelJoint2D[] wheelJoints;
    JointMotor2D frontWheel;
    JointMotor2D backWheel;

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private float deceleration = -800f;
    private float gravity = 9.8f;
    public float angleCar = 0;
    public float acceleration = 1000f;
    public float maxSpeed = -16000f;
    public float maxBackSpeed = 800f;
    public float brakeForce = 1000f;
    public float wheelSize;
    public float rotationTorque = .001f; // Adjust this value to control the rotation responsiveness
    public bool grounded = false;
    public LayerMask ground;
    public Transform bWheel;

    void Start()
    {
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        frontWheel = wheelJoints[0].motor;
        backWheel = wheelJoints[1].motor;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component

    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(bWheel.transform.position, wheelSize, ground);

        angleCar = transform.localEulerAngles.z;
        if (angleCar > 180) angleCar = angleCar - 360;

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Adjusted rotation logic
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(rotationTorque); // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-rotationTorque); // Rotate right
        }

        // Adjusted acceleration and deceleration logic
        if (Input.GetKey(KeyCode.W))
        {
            // Accelerate
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - acceleration * Time.deltaTime, maxSpeed, 0); // Adjust for acceleration in the positive direction
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Decelerate or reverse accelerate
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deceleration * Time.deltaTime, 0, maxBackSpeed); // Adjust for deceleration or reversing
        }
        else
        {
            backWheel.motorSpeed -= backWheel.motorSpeed * 1 / 100;
        }

        // Apply additional downward force for traction
        float downwardForce = 20f; // Adjust this value as needed
        if (grounded)
        {
            rb.AddForce(Vector2.down * downwardForce, ForceMode2D.Force);
        }

        // Update wheel motors
        frontWheel = backWheel;
        wheelJoints[0].motor = backWheel;
        wheelJoints[1].motor = frontWheel;
    }
}