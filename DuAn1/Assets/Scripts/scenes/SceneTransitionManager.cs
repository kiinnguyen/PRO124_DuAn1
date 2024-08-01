using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    [SerializeField]
    Animator animator;
    public string targetSceneName = "Scene3";



    public void TransitionToScene(string sceneName)
    {
        targetSceneName = sceneName;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(targetSceneName);
        animator.SetTrigger("Start");
    }
}