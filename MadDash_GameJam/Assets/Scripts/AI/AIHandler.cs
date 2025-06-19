using System.Collections;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

    [SerializeField]
    LayerMask otherCarsLayerMask;

    [SerializeField]
    MeshCollider meshCollider;

    RaycastHit[] raycastHits = new RaycastHit[1];
    bool isCarAhead = false;
    int drivingInLane = 0;

    WaitForSeconds wait = new WaitForSeconds(0.2f);

    private void Awake()
    {
        if (CompareTag("Player"))
        {
            Destroy(this);
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdateLessOften());
    }

    // Update is called once per frame
    void Update()
    {
        float accelerationInput = 1.0f;

        float steerInput = 0.0f;

        if (isCarAhead)
        {
            accelerationInput = -1.0f;
        }

        float desiredPositionX = DrivingPosition.CarLanes[drivingInLane];
        float difference = desiredPositionX - transform.position.x;

        if (Mathf.Abs(difference) > 0.1f)
        {
            steerInput = difference;
        }

        steerInput = Mathf.Clamp(steerInput, -1.0f, 1.0f);

        carHandler.SetInput(new Vector2 (steerInput, accelerationInput));
    }

    IEnumerator UpdateLessOften()
    {
        while (true)
        {
            isCarAhead = CheckIfOtherCarsAreAhead();
            yield return wait;
        }
    }

    bool CheckIfOtherCarsAreAhead()
    {
        meshCollider.enabled = false;
        int numberOfHits = 0;
        try
        {
            numberOfHits = Physics.BoxCastNonAlloc(transform.position, Vector3.one * 0.25f, transform.forward,
                raycastHits, Quaternion.identity, 2, otherCarsLayerMask);
        }
        finally
        {
            meshCollider.enabled = true;
        }

        return numberOfHits > 0;
    }

    private void OnEnable()
    {
        carHandler.SetMaxSpeed(Random.Range(2, 4));
        drivingInLane = Random.Range(0, DrivingPosition.CarLanes.Length);
    }
} 
