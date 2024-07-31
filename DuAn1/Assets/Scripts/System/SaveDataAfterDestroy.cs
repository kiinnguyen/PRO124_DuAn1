using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataAfterDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        PlayerData.Instance.SavePlayerData();
    }
}
