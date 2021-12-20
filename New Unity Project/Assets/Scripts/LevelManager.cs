using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // include SceneManagement libraries

public class LevelManager : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 2.5f;
    
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        //scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadCo("GameOver", gameOverDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    IEnumerator WaitAndLoadCo(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
