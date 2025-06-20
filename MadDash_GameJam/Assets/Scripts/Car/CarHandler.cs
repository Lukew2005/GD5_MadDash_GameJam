using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform gameModel;
    [SerializeField] ExplodeHandler explodeHandler;
    [SerializeField] FuelGauge fuelGauge;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("SFX")]
    [SerializeField] AudioSource carEngineAS;
    [SerializeField] AnimationCurve carPitchAnimationCurve;
    [SerializeField] AudioSource carSkidAS;
    [SerializeField] AudioSource carCrashAS;

    // Max values
    float maxSteeringVelocity = 2f;
    float maxForwardVelocity = 30f;

    // Multipliers
    float accelerationMultiplier = 3f;
    float brakeMultiplier = 15f;
    float steeringMultiplier = 5f;

    // Input
    Vector2 input = Vector2.zero;

    // States
    bool isExploded = false;
    bool isPlayer = true;
    float carMaxSpeedPercentage = 0;

    // Score
    Vector3 startPos;
    float distanceTravelled = 0f;
    float score;

    public float speed => rb.linearVelocity.magnitude;

    private void Start()
    {
        isPlayer = CompareTag("Player");
        startPos = transform.position;

        if (isPlayer && carEngineAS != null)
            carEngineAS.Play();
    }

    private void Update()
    {
        if (isExploded)
        {
            FadeOutCarAudio();
            return;
        }

        if (fuelGauge && fuelGauge.currentFuel < 1)
        {
            Debug.Log("Outta Fuel!");
            Explode();
            return;
        }

        // Rotate model when turning
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);

        UpdateCarAudio();
    }

    private void FixedUpdate()
    {
        if (isExploded)
        {
            if (fuelGauge) fuelGauge.currentFuel = 0;

            rb.linearDamping = Mathf.Clamp(rb.linearVelocity.z * 0.1f, 1.5f, 10f);
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
            Brake();
        }

        Steer();

        if (rb.linearVelocity.z < 0)
            rb.linearVelocity = Vector3.zero;

        UpdateScore();
    }

    void Accelerate()
    {
        rb.linearDamping = 0;
        if (rb.linearVelocity.z < maxForwardVelocity)
            rb.AddForce(accelerationMultiplier * input.y * transform.forward);
    }

    void Brake()
    {
        if (rb.linearVelocity.z >= 0)
            rb.AddForce(brakeMultiplier * input.y * transform.forward);
    }

    void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            float steerLimit = Mathf.Clamp01(rb.linearVelocity.z / 5f);
            rb.AddForce(steerLimit * steeringMultiplier * input.x * transform.right);

            float normalizedX = Mathf.Clamp(rb.linearVelocity.x / maxSteeringVelocity, -1f, 1f);
            rb.linearVelocity = new Vector3(normalizedX * maxSteeringVelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0, 0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    void UpdateScore()
    {
        float distanceFromLast = Vector3.Distance(transform.position, startPos);
        distanceTravelled += distanceFromLast;
        startPos = transform.position;

        if (transform.position.x > -1 && transform.position.x < 1)
        {
            float currentSpeed = rb.linearVelocity.magnitude;
            score += currentSpeed * distanceFromLast * Time.fixedDeltaTime;
        }

        if (scoreText)
            scoreText.text = $"Score: {Mathf.RoundToInt(score)}";
    }

    void UpdateCarAudio()
    {
        if (!isPlayer) return;

        carMaxSpeedPercentage = rb.linearVelocity.z / maxForwardVelocity;
        if (carEngineAS) carEngineAS.pitch = carPitchAnimationCurve.Evaluate(carMaxSpeedPercentage);

        if (input.y < 0 && carMaxSpeedPercentage > 0.2f)
        {
            if (!carSkidAS.isPlaying)
                carSkidAS.Play();

            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 1.0f, Time.deltaTime * 10);
        }
        else
        {
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 0f, Time.deltaTime * 30);
        }
    }

    void FadeOutCarAudio()
    {
        if (!isPlayer) return;

        if (carEngineAS)
            carEngineAS.volume = Mathf.Lerp(carEngineAS.volume, 0f, Time.deltaTime * 10);
        if (carSkidAS)
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 0f, Time.deltaTime * 10);
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        maxForwardVelocity = maxSpeed;
    }

    void Explode()
    {
        if (isExploded) return;

        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);
        isExploded = true;

        if (carCrashAS)
        {
            carCrashAS.volume = Mathf.Clamp(carMaxSpeedPercentage, 0.25f, 1.0f);
            carCrashAS.pitch = Mathf.Clamp(carMaxSpeedPercentage, 0.3f, 1.0f);
            carCrashAS.Play();
        }

        StartCoroutine(SlowDownTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlayer &&
            (collision.transform.root.CompareTag("Untagged") || collision.transform.root.CompareTag("AICar")))
        {
            return;
        }




        Explode();
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