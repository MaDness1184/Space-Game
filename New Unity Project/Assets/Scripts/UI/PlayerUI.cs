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

    [Header("Ability Bar")]
    [SerializeField] Image abilityBarImage;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        playerMaxHealth = playerHealth.GetHealth(); // slider maximum value = player's heath
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = (float)playerHealth.GetHealth() / (float)playerMaxHealth; // Update Player health each frame
        //scoreText.text = scoreKeeper.GetCurrentScore().ToString("0000"); // Update Player score each frame / ToString("000") adds leading 0s
    }
}
