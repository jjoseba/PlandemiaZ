/*
	Copyright (C) 2020 Anarres

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/> 
*/

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
