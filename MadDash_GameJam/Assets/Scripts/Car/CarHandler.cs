using System;
using System.Security.Cryptography;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

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
    }

    void Accelerate()
    {
        rb.linearDamping = 0;

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
            rb.AddForce(steeringMultiplier * input.x * transform.right);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        
        input = inputVector;
    }
}
