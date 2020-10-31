using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float fordwardForce = 100f;
    public float steeringForce = 100f;
    public float maxSpeed = 200f;

    private float currentSpeed;


    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(steeringForce * Time.deltaTime, 0, 0);
            
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-steeringForce * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("w"))
        {
            currentSpeed = rb.velocity.magnitude;
            // set the force on to be closer and closer to zero as speed increases
            float accelForce = fordwardForce * maxSpeed - (currentSpeed / maxSpeed);
            rb.AddRelativeForce(0, 0, accelForce * Time.deltaTime);
        }

        
    }
}
