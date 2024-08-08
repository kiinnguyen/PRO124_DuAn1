using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : Singleton<UILoading>
{
    [Header("UI Loading")]
    [SerializeField] string sceneName;


    public void LoadScene(string name)
    {
        sceneName = name;
        SceneManager.LoadScene(1);
    }


    public void ReadyToLoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
