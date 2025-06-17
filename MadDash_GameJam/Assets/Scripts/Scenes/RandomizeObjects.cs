using UnityEngine;

public class RandomizeObjects : MonoBehaviour
{
    [SerializeField]
    Vector3 localRotationMin = new Vector3(-10, 0, -10);
    [SerializeField]
    Vector3 localRotationMax = new Vector3(10, 360, 10);

    [SerializeField]
    float localScaleMultiplierMin = 0.8f;
    [SerializeField]
    float localScaleMultiplierMax = 1.5f;

    Vector3 localScaleOriginal = Vector3.zero;

    void Start()
    {
        localScaleOriginal = transform.localScale;
    }

    void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(
            Random.Range(localRotationMin.x, localRotationMax.x),
            Random.Range(localRotationMin.y, localRotationMax.y),
            Random.Range(localRotationMin.z, localRotationMax.z));

        transform.localScale = Random.Range(localScaleMultiplierMin, localScaleMultiplierMax) * localScaleOriginal;
    }
}
