using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // To access TMP_Text and Button types
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;

    public int maxItems = 4;

    public TMP_Text healthText;
    public TMP_Text itemText;
    public TMP_Text progressText;

    public Button winButton;
    public Button lossButton;

    private void Start()
    {
        itemText.text += _itemsCollected;
        healthText.text += _playerHP;
    }

    public void RestartScene()
    {
        // Load the first scene to restart the game
        SceneManager.LoadScene(0);
        // Unpause the game
        Time.timeScale = 1f;
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
                winButton.gameObject.SetActive(true);

                // Pause the game to prevent any movement
                Time.timeScale = 0f;
            }
            else
            {
                progressText.text = $"Item found, only {maxItems - _itemsCollected} more!";
            }

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

            // Check if the player should be dead
            if(_playerHP <= 0)
            {
                // The player died
                progressText.text = "You want another life with that?";
                lossButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                // The player took damage
                progressText.text = "Ouch... that's got to hurt!";
            }
        }
    }
}
