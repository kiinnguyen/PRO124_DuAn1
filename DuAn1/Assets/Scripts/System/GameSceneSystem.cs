using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSystem : MonoBehaviour
{
    private void Awake()
    {
        GameData.Instance.GameSceneData();
    }

    public void UpdateGameSceneData()
    {

    }
}
