using System;
using System.Collections;
using UnityEngine;

public class CarHandler : MonoBehaviour
{

    //For The Fuel
    public FuelGauge fuelGauge;
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform gameModel;

    [SerializeField]
    ExplodeHandler explodeHandler;

    // Max values
    float maxSteeringVelocity = 2;
    float maxForwardVelocity = 30;

    // Mulipliers
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    // Input
    Vector2 input = Vector2.zero;

    // States
    bool isExploded = false;
    bool isPlayer = true;

    private void Start()
    {
        isPlayer = CompareTag("Player");
    }

    void Update()
    {
        if (isExploded)
        {
            return;
        }


        float fuelLeft = fuelGauge.currentFuel;
        if (fuelLeft < 1)
        {

            Debug.Log("Outta Fuel!");
            //(Luke) Added That The Car Will Explode If You Have No Fuel
            Vector3 velocity = rb.linearVelocity;
            explodeHandler.Explode(velocity * 45);

            isExploded = true;
        }

        // Rotate the car model when turning
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);


    }

    void FixedUpdate()
    {
        if (isExploded)
        {
            rb.linearDamping = rb.linearVelocity.z * 0.1f;
            rb.linearDamping = Mathf.Clamp(rb.linearDamping, 1.5f, 10);

            rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(0, 0, transform.position.z), Time.deltaTime * 0.5f));
            return;
        }

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

    public void SetMaxSpeed(float maxSpeed)
    {
        //Debug.Log($"Setting max speed to: {maxSpeed}, for Car Type: {rb.transform.name}!");
        maxForwardVelocity = maxSpeed;
    }

    IEnumerator SlowDownTime()
    {
        try
        {
            while (Time.timeScale > 0.2f)
            {
                Time.timeScale -= Time.deltaTime * 2;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            while (Time.timeScale < 1.0f)
            {
                Time.timeScale += Time.deltaTime;
                yield return null;
            }
        }
        finally
        {
            Time.timeScale = 1.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlayer)
        {
            if (collision.transform.root.CompareTag("Untagged") ||
                collision.transform.root.CompareTag("AICar"))
            {
                return;
            }
        }








        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);

        isExploded = true;

        StartCoroutine(SlowDownTime());
    }
    



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Debug.Log("Collected Fuel");
            Destroy(other.gameObject);
            int fuelToAdd = UnityEngine.Random.Range(30, 50);
            fuelGauge.currentFuel += fuelToAdd;
        }
    }
}
