using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCar : MonoBehaviour
{
    [Header("Cars")]
    [SerializeField] private Sprite[] cars;
    
    [Header("Fuel")]
    [SerializeField] private Slider fuelSlider;
    public float maxFuel = 100.0f;
    public float fuelConsumptionRate = 1.0f;
    public float refuelRate = 10.0f;
    [SerializeField] private GameObject gameOverMenu;

    [Header("Drive Back")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    public float driveBackTime = 1f;
    public float checkRadius;
    
    [Header("Car Controller")]
    public float acceleration = 5.0f;      // Ускорение машинки
    public float steeringSpeed = 5.0f;    // Скорость поворота
    public float maxSpeed = 10.0f;        // Максимальная скорость машинки
    public float driftIntensity = 0.2f;   // Интенсивность заноса
    public float grip = 1.0f;             // Сцепление с дорогой
    public Transform centerOfMass;        // Центр массы машинки

    private float currentFuel = 0.0f;
    private float currentSpeed = 0.0f;
    private float currentSteering = 0.0f;
    private Rigidbody2D rb;
    private float verticalInput;
    private bool driveForward;
    private float horizontalInput;
    private bool gameOver;

    void Start()
    {
        gameOver = false;
        horizontalInput = 0;
        GetComponent<SpriteRenderer>().sprite = cars[PlayerPrefs.GetInt("selectedItem", 0)];
        driveForward = true;
        verticalInput = 1;
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = centerOfMass.localPosition;
        
        currentFuel = maxFuel;
        UpdateFuelSlider();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer))
        {
            StartCoroutine(DriveBack());
        }

        float accelerationInput = verticalInput * acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed + accelerationInput * Time.deltaTime, 0, maxSpeed);

        currentSteering = Mathf.Lerp(currentSteering, -horizontalInput * steeringSpeed, Time.deltaTime * 2);

        float drift = currentSteering * driftIntensity * (1 - grip);
        rb.AddTorque(drift);
        
        float fuelUsage = fuelConsumptionRate * Time.deltaTime;
        ConsumeFuel(fuelUsage);
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            rb.angularVelocity = currentSteering * currentSpeed;
            if (driveForward)
                rb.velocity = transform.up * currentSpeed;
            else
                rb.velocity = -transform.up * currentSpeed;
        }
    }

    IEnumerator DriveBack()
    {
        driveForward = false;
        yield return new WaitForSeconds(driveBackTime);
        driveForward = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
    
    void UpdateFuelSlider()
    {
        fuelSlider.value = currentFuel;
    }
    
    public void Refuel()
    {
        currentFuel = maxFuel;
        UpdateFuelSlider();
    }
    
    private void ConsumeFuel(float amount)
    {
        currentFuel -= amount;
        if (currentFuel < 0)
        {
            currentFuel = 0;
            GameOver();
        }
        UpdateFuelSlider();
    }

    private void GameOver()
    {
        gameOver = true;
        rb.velocity = Vector2.zero;
        gameOverMenu.SetActive(true);
    }

    public void RightArrow()
    {
        horizontalInput = 1;
    }
    public void LeftArrow()
    {
        horizontalInput = -1;
    }
    public void StopMoving()
    {
        horizontalInput = 0;
    }
}
