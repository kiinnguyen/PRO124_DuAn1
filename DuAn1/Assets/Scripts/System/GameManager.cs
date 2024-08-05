using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static UnityEvent OnPause = new UnityEvent();
    public static UnityEvent OnResume = new UnityEvent();

    private bool isPaused = false;

    // UI Loading
    public void LoadScene(string sceneNameInput)
    {
        SceneManager.LoadScene(sceneNameInput);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckingProgessLoading(float progessValue)
    {
        if (progessValue >= 100)
        {
            Debug.Log("Done");
            SceneManager.LoadScene(2);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        OnPause?.Invoke();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isPaused = false;
        OnResume?.Invoke();
        Debug.Log("Game Resumed");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player has died!");
        PauseGame();
    }


    // Game Data

    
}
