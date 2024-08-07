using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScenes : MonoBehaviour
{
    public float creditscene = 20f;
    public string sceneName = "Menu";

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = creditscene; 
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene(sceneName);
            }
        }

    }
}
