using UnityEngine;

public class Gate : MonoBehaviour
{
    public sceneManager transitionManager; 
    public string targetSceneName = "Scene3"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transitionManager.TransitionToScene(targetSceneName);
        }
    }
}