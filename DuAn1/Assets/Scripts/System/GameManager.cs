using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Player player;

    [SerializeField] string userName;


    [Header("Font")]
    [SerializeField] Font newFont;

    string dataPath = "Assets/Data/playerData.json";


    private void Start()
    {
        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text text in texts)
        {
            text.font = newFont;
            text.fontStyle = FontStyle.Normal;
        }

    }

    public void SetUserName(Text name)
    {
        userName = name.text;
    }

    public string GetUserName()
    {
        return userName;
    }

    // UI Loading
    private int sceneIndex;
    
    public void LoadScene(string sceneNameInput)
    {
        
    }

    public void CheckingProgessLoading(float progessValue)
    {
        if (progessValue >= 100)
        {
            Debug.Log("Done");
            SceneManager.LoadScene(2);
        }
    }

    // DATA

    public void SetData()
    {

    }

    public void GetData()
    {

    }

}
