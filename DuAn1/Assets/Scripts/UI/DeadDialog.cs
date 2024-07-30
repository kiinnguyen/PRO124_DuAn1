using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadDialog : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image container;

    private bool isFade = false;
    private bool isText = false;
    void Start()
    {
        if (text != null && container != null)
        {
            StartCoroutine(DeadDialogCoroutine());
        }
    }

    IEnumerator DeadDialogCoroutine()
    {
        while (!isFade && !isText)
        {
            Color colorContainer = container.color;
            Color colorText = text.color;
            float startAlpha = 0f;
            float endAlpha = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                colorContainer.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 2f);
                container.color = colorContainer;
                colorText.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 2f);
                text.color = colorContainer;
                yield return null;
            }

            colorContainer.a = endAlpha;
            container.color = colorContainer;
            colorText.a = endAlpha;
            text.color = colorText;
            isFade = true;
            isText = true;
        }


        GameManager.Instance.RestartLevel();
        yield return null;
    }
}
