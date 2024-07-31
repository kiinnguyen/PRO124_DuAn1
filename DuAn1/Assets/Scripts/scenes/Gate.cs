using UnityEngine;

public class Gate : MonoBehaviour
{
    public sceneManager transitionManager;
    private PlayerData playerData;
    public string targetSceneName = "Scene3";

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
