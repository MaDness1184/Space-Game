using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Private
    int currentScore = 0;
    static ScoreKeeper scoreKeeperInstance;

    // Singleton instance
    public ScoreKeeper GetInstance() // USE CAREFULLY
    {
        return scoreKeeperInstance;
    }

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (scoreKeeperInstance != null)
        {
            gameObject.SetActive(false); // Disable before destoying so that objects wont try to access it befor it's destroyed
            Destroy(gameObject);
        }
        else
        {
            scoreKeeperInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetCurrentScore() // returns current score
    {
        return currentScore;
    }

    public void UpdateCurrentScore(int m_value) // Updates score with param value
    {
        currentScore += m_value;
        Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore() // resets score back to 0
    {
        currentScore = 0;
    }
}
