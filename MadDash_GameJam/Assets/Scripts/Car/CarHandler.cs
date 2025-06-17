using System;
using System.Security.Cryptography;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform gameModel;

    // Max values
    float maxSteeringVelocity = 2;
    float maxForwardVelocity = 30;

    // Mulipliers
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    // Input
    Vector2 input = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        // Rotate the car model when turning
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
    }

    void FixedUpdate()
    {
        if (input.y > 0)
        {
            Accelerate();
        }
        else if (input.y < 0)
        {
            rb.linearDamping = 0.2f;
        }

        if (input.y < 0)
        {
            Brake();
        }

        Steer();

        if (rb.linearVelocity.z < 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void Accelerate()
    {
        rb.linearDamping = 0;

        // Stay within the speed limit
        if (rb.linearVelocity.z > maxForwardVelocity)
        {
            return;
        }

        rb.AddForce(accelerationMultiplier * input.y * transform.forward);
    }

    void Brake()
    {
        if (rb.linearVelocity.z < 0) return;

        rb.AddForce(brakeMultiplier * input.y * transform.forward);
    }

    void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            // Move the car sideways
            float speedBaseSteerLimint = rb.linearVelocity.z / 5.0f;
            speedBaseSteerLimint = Mathf.Clamp01(speedBaseSteerLimint);

            rb.AddForce(speedBaseSteerLimint * steeringMultiplier * input.x * transform.right);

            // Normalize the X Velocity
            float normalizedXVelocity = rb.linearVelocity.x / maxSteeringVelocity;
            normalizedXVelocity = Mathf.Clamp(normalizedXVelocity, -1.0f, 1.0f);

            // Make sure we stay within the turn speed limit
            rb.linearVelocity = new Vector3(normalizedXVelocity * maxSteeringVelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0, 0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        
        input = inputVector;
    }
}
