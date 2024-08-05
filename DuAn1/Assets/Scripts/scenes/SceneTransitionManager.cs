using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : Singleton<SceneTransitionManager> 
{ 
    public static SceneTransitionManager instance;
    [SerializeField]
    private Animator animator;
    public string targetSceneName = "Scene3";
    public void TransitionToScene(string sceneName)
    {
        if (animator != null)
        {
            targetSceneName = sceneName;
            StartCoroutine(LoadScene());
        }
        else
        {
            Debug.LogError("null");
        }
    }

    IEnumerator LoadScene()
    {
        animator.SetTrigger("End"); 
        yield return new WaitForSeconds(2);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        animator.SetTrigger("Start");
    }
}
