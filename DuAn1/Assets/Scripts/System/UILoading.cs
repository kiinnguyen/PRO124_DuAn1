using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [Header("UI Loading")]
    [SerializeField] Slider progressLoading;
    [SerializeField] float loadingDuration;

    [Header("System")]
    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (progressLoading != null)
        {
            progressLoading.value = progressLoading.minValue;
            StartCoroutine(LoadingCoroutine());
        }
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
    }


    public void UpdateProgressText(Text progessText)
    {
        progessText.text = Mathf.RoundToInt(progressLoading.value).ToString() + "%";
    }

    public void UpdateProgessToSystem()
    {
        gameManager.CheckingProgessLoading(progressLoading.value);
    }
}
