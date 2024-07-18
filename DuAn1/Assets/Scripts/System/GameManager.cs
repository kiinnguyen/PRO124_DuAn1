using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    Chức năng: 
  Khởi tạo game: Thiết lập trạng thái ban đầu của game khi người chơi bắt đầu một trò chơi mới.

Quản lý cấp độ (Level Management): Xử lý chuyển đổi giữa các cấp độ khác nhau trong game.

Lưu và tải game: Quản lý việc lưu và tải dữ liệu game.

Quản lý thời gian (Time Management): Điều khiển thời gian trong game như tạm dừng (pause) và tiếp tục (resume).

Xử lý sự kiện toàn cục: Xử lý các sự kiện quan trọng trong game như người chơi chết, chiến thắng, hoặc thất bại.
 */


public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Player player;

    [SerializeField] string userName;


    [Header("Font")]
    [SerializeField] Font newFont;

    string dataPath = "Assets/Data/playerData.json";

    public void SetUserName(Text name)
    {
        userName = name.text;
        Debug.Log("Get UserName Successfully");
    }

    public string GetUserName()
    {
        return userName;
    }

    // UI Loading
    
    public void LoadScene(string sceneNameInput)
    {
        SceneManager.LoadScene(sceneNameInput);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void CheckingProgessLoading(float progessValue)
    {
        if (progessValue >= 100)
        {
            Debug.Log("Done");
            SceneManager.LoadScene(2);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("Game Resumed");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Player has died!");
        RestartLevel();
    }


}
