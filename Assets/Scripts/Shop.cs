using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int coins;

    [SerializeField] private Text coinsText;
    [SerializeField] private Image[] buttons;
    [SerializeField] private Sprite[] nonSelectedItems;
    [SerializeField] private Sprite[] selectedItems;
    [SerializeField] private Sprite lockedItem;

    private int[] items = new int[] { 1, 0, 0, 0, 0, 0 };
    private string[] itemsName = new[] { "item1", "item2", "item3", "item4", "item5", "item6" };

    public int selectedItem;
    void Start()
    {
        coins = PlayerPrefs.GetInt("coins");

        selectedItem = PlayerPrefs.GetInt("selectedItem", 0);

        items[0] = PlayerPrefs.GetInt(itemsName[0], 1);
        items[1] = PlayerPrefs.GetInt(itemsName[1], 0);
        items[2] = PlayerPrefs.GetInt(itemsName[2], 0);
        items[3] = PlayerPrefs.GetInt(itemsName[3], 0);
        items[4] = PlayerPrefs.GetInt(itemsName[4], 0);
        items[5] = PlayerPrefs.GetInt(itemsName[5], 0);
        
        PlayerPrefs.SetInt("coins", coins);
        
        PlayerPrefs.SetInt("selectedItem", selectedItem);
        
        PlayerPrefs.SetInt(itemsName[0], items[0]);
        PlayerPrefs.SetInt(itemsName[1], items[1]);
        PlayerPrefs.SetInt(itemsName[2], items[2]);
        PlayerPrefs.SetInt(itemsName[3], items[3]);
        PlayerPrefs.SetInt(itemsName[4], items[4]);
        PlayerPrefs.SetInt(itemsName[5], items[5]);

        UpdateUI();
    }

    public void OpenBall(int id)
    {
        string itemId = id == 0 ? itemsName[0] : id == 1 ? itemsName[1] : id == 2 ? itemsName[2] :
            id == 3 ? itemsName[3] : id == 4 ? itemsName[4] : id == 5 ? itemsName[5] : String.Empty;

        if (PlayerPrefs.GetInt(itemId) == 0)
        {
            if (coins >= 50)
            {
                coins -= 50;
                PlayerPrefs.SetInt("coins", coins);

                items[id] = 1;

                selectedItem = id;
                PlayerPrefs.SetInt("selectedItem", selectedItem);
            
                PlayerPrefs.SetInt(itemId, 1);
            }
            else
            {
                items[id] = 0;
                PlayerPrefs.SetInt(itemId, 0);
                Debug.Log("No Money");
            }
        }
        else
        {
            selectedItem = id;
            PlayerPrefs.SetInt("selectedItem", selectedItem);
            Debug.Log("Already Has");
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinsText.text = coins.ToString();
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].sprite = items[i] == 1 ? nonSelectedItems[i] : lockedItem;
        }

        buttons[selectedItem].sprite = selectedItems[selectedItem];
    }
}
