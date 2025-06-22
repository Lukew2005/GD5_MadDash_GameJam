using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    public CarHandler carHandler;
    public Image fuelFillImage;
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelBurnRate = 10f;

    void Start()
    {
        currentFuel = maxFuel;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    currentFuel += 10;
        //}

        float speed = carHandler.speed;

        fuelBurnRate = 1;
        if (speed > 2) fuelBurnRate = 3;
        if (speed > 10) fuelBurnRate = 5;
        if (speed > 15) fuelBurnRate = 7;

        currentFuel -= fuelBurnRate * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);

        float fillAmount = currentFuel / maxFuel;
        fuelFillImage.fillAmount = fillAmount;

        fuelFillImage.color = GetFuelGaugeColour(fillAmount);
    }

    private Color GetFuelGaugeColour(float fillAmount)
    {
        switch (fillAmount)
        {
            case > 0.6f:
                return Color.green;
            case > 0.2f:
                return Color.yellow;
            default:
                return Color.red;
        }
     }
}
