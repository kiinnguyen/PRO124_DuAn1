using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameScene : MonoBehaviour
{
    [Header("Speaker")]
    [SerializeField] AudioSource musicSpeaker;


    public void ReturnMenuScene()
    {
        SceneManager.LoadScene(0);

    }
}
