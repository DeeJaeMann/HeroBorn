using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // To access TMP_Text variable type

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;

    public int maxItems = 4;

    public TMP_Text healthText;
    public TMP_Text itemText;
    public TMP_Text progressText;

    private void Start()
    {
        itemText.text += _itemsCollected;
        healthText.text += _playerHP;
    }

    public int Items
    {
        get { return _itemsCollected; }

        set
        {
            _itemsCollected = value;

            itemText.text = $"Items: {Items}";

            if (_itemsCollected >= maxItems)
            {
                progressText.text = "You've found all the items!";
            }
            else
            {
                progressText.text = $"Item found, only {maxItems - _itemsCollected} more!";
            }

            //Debug.LogFormat($"Items: {_itemsCollected}");
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            healthText.text = $"Health: {HP}";
            Debug.LogFormat($"Lives: {_playerHP}");
        }
    }
}
