﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [Header("UI Loading")]
    [SerializeField] Slider progressLoading;
    [SerializeField] float loadingDuration = 10f;

    void Start()
    {
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


    public void UpdateProgressText(TextMeshProUGUI progessText)
    {
        progessText.text = Mathf.RoundToInt(progressLoading.value).ToString() + "%";
    }
}