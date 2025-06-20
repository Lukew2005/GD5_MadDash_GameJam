using UnityEngine;

public class fuelCanSpawner : MonoBehaviour
{
    public GameObject fuelCanPrefab;
    public float spawnDistance = 60f;
    public float spawnInterval = 3f;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnFuelCan), 2f, spawnInterval);
    }

    void SpawnFuelCan()
    {
        float randomX = Random.Range(-1f, 0.8f);
        Vector3 spawnPos = new Vector3(randomX, 0f, player.position.z + spawnDistance);

        Quaternion rotation = Quaternion.Euler(0, 90, 0);

        Instantiate(fuelCanPrefab, spawnPos, rotation);
    }






}