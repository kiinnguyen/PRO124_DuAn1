using UnityEngine;
using UnityEngine.SceneManagement;
public class ChucNangUI : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        SceneManager.LoadScene(0);
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

    }
}
