using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public SceneTransitionManager transitionManager;  
    private PlayerData playerData;
    public string targetSceneName = "Cave";

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerData.SavePlayerData();
            transitionManager.TransitionToScene(targetSceneName);
        }
    }

}
