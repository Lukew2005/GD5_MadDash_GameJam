using System.Collections;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionsPrefabs;

    GameObject[] sectionsPool = new GameObject[20];
    GameObject[] sections = new GameObject[10];

    Transform playerCarTransform;
    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    const float sectionLength = 26;
    float nextZ = 0f; // Track where to place the next section

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;

            if (prefabIndex > sectionsPrefabs.Length - 1)
            {
                prefabIndex = 0;
            }
        }

        // Add the first sections to the road
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject randomSection = GetRandomSectionFromPool();

            randomSection.transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, i * sectionLength);
            randomSection.SetActive(true);

            sections[i] = randomSection;
        }

        nextZ = sections.Length * sectionLength; // Set nextZ after initial placement

        StartCoroutine(UpdateLessOften());
    }

    IEnumerator UpdateLessOften()
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLength)
            {
                sections[i].SetActive(false);
                sections[i] = GetRandomSectionFromPool();
                sections[i].transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, nextZ);
                sections[i].SetActive(true);
                nextZ += sectionLength;
            }
        }
    }

    GameObject GetRandomSectionFromPool()
    {
        for (int attempt = 0; attempt < sectionsPool.Length; attempt++)
        {
            int randomIndex = Random.Range(0, sectionsPool.Length);

            if (!sectionsPool[randomIndex].activeInHierarchy)
            {
                return sectionsPool[randomIndex];
            }
        }

        Debug.LogWarning("All sections are active! Consider increasing pool size.");
        return sectionsPool[Random.Range(0, sectionsPool.Length)];
    }
}