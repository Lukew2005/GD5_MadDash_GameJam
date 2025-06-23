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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentFuel += 10;
        }

        float speed = carHandler.speed;

        if (speed > 2)
        {
            fuelBurnRate = 3;
            currentFuel -= fuelBurnRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        }

        if (speed > 10)
        {
            fuelBurnRate = 5;
            currentFuel -= fuelBurnRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        }

        if (speed > 15)
        {
            fuelBurnRate = 7;
            currentFuel -= fuelBurnRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        }

        float fillAmount = currentFuel / maxFuel;
        fuelFillImage.fillAmount = fillAmount;


        if (currentFuel > 60)
        {
            fuelFillImage.color = Color.green;
        }

        if (currentFuel > 20 & currentFuel < 61)
        {
            fuelFillImage.color = Color.yellow;
        }

        if (currentFuel < 21)
        {
            fuelFillImage.color = Color.red;
        }
    }
}
