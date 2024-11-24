using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelArea : MonoBehaviour
{
    [SerializeField] private Sprite[] progress;
    
    public float timeToRefuel;
    public int scoreValue = 10;
    
    private SpriteRenderer spriteRenderer;
    private bool isOut;
    private Score score;
    private FuelAreaSpawner fuelAreaSpawner;
    private bool used;
    private bool spawnedNext;

    private void Start()
    {
        spawnedNext = false;
        used = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = FindObjectOfType<Score>().GetComponent<Score>();
        fuelAreaSpawner = FindObjectOfType<FuelAreaSpawner>().GetComponent<FuelAreaSpawner>();
        spriteRenderer.sprite = progress[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoveCar car = other.GetComponent<MoveCar>();
            if (car != null)
            {
                isOut = false;
                if (!spawnedNext)
                {
                    fuelAreaSpawner.SpawnFuelArea(transform.position);
                    spawnedNext = true;
                }
                if(!used)
                    StartCoroutine(Refuel(car));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !used)
        {
            MoveCar car = other.GetComponent<MoveCar>();
            if (car != null)
            {
                if(!used)
                    isOut = true;
            }
        }
    }

    IEnumerator Refuel(MoveCar car)
    {
        yield return new WaitForSeconds(timeToRefuel / 2f);
        spriteRenderer.sprite = progress[1];
        yield return new WaitForSeconds(timeToRefuel);
        if (isOut)
        {
            spriteRenderer.sprite = progress[0];
            yield break;
        }
        spriteRenderer.sprite = progress[2];
        car.Refuel();
        score.IncreaseScore(scoreValue);
        used = true;
    }
}
