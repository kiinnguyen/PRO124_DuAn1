using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgessLoading : MonoBehaviour
{
    [SerializeField] Slider progressLoading;
    [SerializeField]  float loadingDuration = 4f;
    UILoading uiLoading;

    void Start()
    {
        uiLoading = FindObjectOfType<UILoading>();
        StartCoroutine(LoadingCoroutine());
    }


    IEnumerator LoadingCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            progressLoading.value = Mathf.Lerp(progressLoading.minValue, progressLoading.maxValue, elapsedTime / loadingDuration);

            yield return null;
        }

        progressLoading.value = progressLoading.maxValue;

        if (uiLoading != null)
        {
            uiLoading.ReadyToLoadScene();
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void UpdateProgressText(Text progessText)
    {
        progessText.text = Mathf.RoundToInt(progressLoading.value).ToString() + "%";
    }
}
