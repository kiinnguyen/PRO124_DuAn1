using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    public void ReturnMenu()
    {
        GameManager.Instance.LoadScene("Menu");
    }
}
