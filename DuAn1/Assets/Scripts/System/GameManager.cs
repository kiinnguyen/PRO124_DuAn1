using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static UnityEvent OnPause = new UnityEvent();
    public static UnityEvent OnResume = new UnityEvent();

    public static UnityEvent OnSaveData = new UnityEvent();
    public static UnityEvent OnUploadData = new UnityEvent();
    private bool isPaused = false;

    private void Start()
    {
        Debug.Log("Has Key timer:" + PlayerPrefs.HasKey("timer"));
    }


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

    public void SaveData()
    {
        OnSaveData?.Invoke();
        Debug.Log("Save Data");
    }

    public void UploadData()
    {
        OnUploadData?.Invoke();
        Debug.Log("Upload Data");
    }
    public void OnPlayerDeath()
    {
        Debug.Log("Player has died!");
        PauseGame();
    }


    // Game Data

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData.Instance.SavePlayerData();
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerData.Instance.SavePlayerData();
            SceneManager.LoadScene(2);
        }
    }
}
