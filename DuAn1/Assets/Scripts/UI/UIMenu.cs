using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Header("System")]
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void StartGame()
    {
        // create character 
        SceneManager.LoadScene(1);
    }
}
