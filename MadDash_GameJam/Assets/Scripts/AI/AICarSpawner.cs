using System;
using System.Collections;
using UnityEngine;

public class AICarSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] carAIPrefabs;

    GameObject[] carAIPool = new GameObject[20];

    WaitForSeconds wait = new WaitForSeconds(2.0f);

    Transform playerCarTransform;

    [SerializeField]
    LayerMask otherCarsLayerMask;

    Collider[] overlappedCheckCollider = new Collider[1];

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        for (int i = 0; i < carAIPool.Length; i++)
        {
            carAIPool[i] = Instantiate(carAIPrefabs[prefabIndex]);
            carAIPool[i].SetActive(false);

            prefabIndex++;

            // Loop the prefab index if we run out of prefabs
            if (prefabIndex >= carAIPrefabs.Length)
            {
                prefabIndex = 0;
            }
        }

        SpawnNewCars(60);
        SpawnNewCars(80);
        StartCoroutine(UpdateLessOften());
    }

    IEnumerator UpdateLessOften()
    {
        while (true)
        {
            CleanUpCarsBeyondView();
            SpawnNewCars();
            yield return wait;
        }
    }

    private void CleanUpCarsBeyondView()
    {
        foreach (GameObject aiCar in carAIPool)
        {
            if (!aiCar.activeInHierarchy)
            {
                continue;
            }

            // Check if AI car is too far ahead
            if (aiCar.transform.position.z - playerCarTransform.transform.position.z > 200)
            {
                aiCar.SetActive(false);
            }

            // Check if AI car is too far behind
            if (aiCar.transform.position.z - playerCarTransform.transform.position.z < -50)
            {
                aiCar.SetActive(false);
            }
        }
    }

    private void SpawnNewCars(int distanceAhead = 100)
    {
        GameObject carToSpawn = null;
        float yPos = 0.0f;

        foreach (GameObject aiCar in carAIPool)
        {
            if (aiCar.activeInHierarchy)
            {
                continue;
            }

            carToSpawn = aiCar;
            yPos = aiCar.transform.position.y;
            break;
        }

        if (carToSpawn == null)
        {
            return;
        }

        Vector3 spawnPosition = new Vector3(0, yPos, 
            playerCarTransform.transform.position.z + distanceAhead);

        if (Physics.OverlapBoxNonAlloc(spawnPosition, Vector3.one * 2, overlappedCheckCollider, 
            Quaternion.identity, otherCarsLayerMask) > 0)
        {
            return;
        }

        carToSpawn.transform.position = spawnPosition;
        carToSpawn.SetActive(true);
    }
}
