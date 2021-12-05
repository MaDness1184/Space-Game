using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Access UI Classes of Unity Engine
using TMPro; // Access to the TMP Class

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerHealth playerHealth;

    [Header("Score")]
    //[SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Reference to the scoreKeeper
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth(); // slider maximum value = player's heath
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.GetHealth(); // Update Player health each frame
        //scoreText.text = scoreKeeper.GetCurrentScore().ToString("0000"); // Update Player score each frame / ToString("000") adds leading 0s
    }
}
