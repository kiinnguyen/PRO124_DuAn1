using UnityEngine;

public class Gate : MonoBehaviour
{
    public sceneManager transitionManager;
    public string targetSceneName = "Scene3";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transitionManager.TransitionToScene(targetSceneName);
        }
    }

}
