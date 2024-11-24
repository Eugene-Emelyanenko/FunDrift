using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coins;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            Destroy(gameObject);
        }
    }
}
