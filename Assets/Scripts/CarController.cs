using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] private Slider fuelSlider;  
    [SerializeField] private Sprite[] cars;
    public float maxSpeed;
    public float acc;
    public float steering;
    public float maxFuel = 100.0f;
    public float fuelConsumptionRate = 1.0f;
    public float refuelRate = 10.0f;

    private float currentFuel;
    private Rigidbody2D rb;
    private float x;
    private float y = 1;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = cars[PlayerPrefs.GetInt("selectedItem", 0)];
        rb = GetComponent<Rigidbody2D>();
        
        currentFuel = maxFuel;
        UpdateFuelSlider();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        
        Vector2 speed = transform.up * (y * acc);
        
        rb.AddForce(speed);


        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (acc > 0)
        {
            if (direction > 0)
            {
                rb.rotation -= x * steering * (rb.velocity.magnitude / maxSpeed);
            }
            else
            {
                rb.rotation += x * steering * (rb.velocity.magnitude / maxSpeed);
            }
        }

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left) * 2.0f);

        Vector2 relativeForce = Vector2.right * driftForce;

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        
        float fuelUsage = fuelConsumptionRate * Time.deltaTime;
        ConsumeFuel(fuelUsage);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Refuel(refuelRate);
        }
    }

    public void Refuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
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
        Debug.Log("Game Over!");
    }
    
    void UpdateFuelSlider()
    {
        fuelSlider.value = currentFuel;
    }
}
