using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void StartGame()
    {
        // create character 
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}
