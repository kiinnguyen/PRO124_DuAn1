using UnityEngine;
using UnityEngine.SceneManagement;

public class UIcode : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        
    }

    public void ReturnGameScene()
    {

    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Setting()
    {
        SceneManager.LoadScene(4);
    }
}
