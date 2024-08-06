using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    /*
        Player: health, food, gold, pos
     
     */

    private GameObject continueButton;
    private void Start()
    {
        continueButton = GameObject.Find("Continue Game");

        if (continueButton != null)
        {
            if (CheckingNullData())
            {
                continueButton.SetActive(false);
            }
            else
            {
                continueButton.SetActive(true);
            }
        }
    }


    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    private bool CheckingNullData()
    {
        if (PlayerPrefs.HasKey("health") ||
            PlayerPrefs.HasKey("food") ||
            PlayerPrefs.HasKey("gold") ||
            PlayerPrefs.HasKey("pos")) return false;
        return true;
    }
}
