using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    [SerializeField]
    private Animator animator;
    public string targetSceneName = "Scene3";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
