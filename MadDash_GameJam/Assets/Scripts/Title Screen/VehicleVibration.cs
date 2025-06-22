using UnityEngine;
using DG.Tweening;

public class VehicleVibration : MonoBehaviour
{
    public float vibrationStrength = 5f;
    public float vibrationDuration = 0.2f;

    private void Start()
    {
        StartVibration();
    }

    public void StartVibration()
    {
        transform.DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength)).SetLoops(-1, LoopType.Restart);
        //transform.DOShakeRotation(
        // Smooth fade-out
    }

    public void StopVibration()
    {
        DOTween.Kill(transform); // Kill all tweens on the transform
    }
}
