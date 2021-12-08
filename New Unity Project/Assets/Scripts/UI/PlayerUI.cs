using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Access UI Classes of Unity Engine
using TMPro; // Access to the TMP Class

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Image healthBarImage;
    [SerializeField] PlayerHealth playerHealth;
    private int playerMaxHealth = 100;

    [Header("Ability Batteries")]
    [SerializeField] Image[] batteries;
    [SerializeField] Sprite fullBattery;
    [SerializeField] Sprite emptyBattery;
    [SerializeField] FloatReference playerBatteries;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBatteries();
        playerMaxHealth = playerHealth.GetHealth(); // slider maximum value = player's heath
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = (float)playerHealth.GetHealth() / (float)playerMaxHealth; // Update Player health each frame
        //scoreText.text = scoreKeeper.GetCurrentScore().ToString("0000"); // Update Player score each frame / ToString("000") adds leading 0s
    }

    public void InitializeBatteries()
    {
        for (int i = 0; i < playerBatteries.GetValue(); i++)
        {
            batteries[i].gameObject.SetActive(true);
            batteries[i].sprite = fullBattery;
        }
    }

    public void UpdateBatteries()
    {
        for (int i = 0; i < playerBatteries.GetValue(); i++)
        {
            if (i < playerBatteries.GetRuntimeValue() - 1)
            {
                batteries[i].sprite = fullBattery;
            }
            else
            {
                batteries[i].sprite = emptyBattery;
            }
        }
    }
}
