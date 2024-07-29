using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class sceneManager : MonoBehaviour
{
    public Image fadeImage; // ảnh chuyển
    public float fadeDuration = 1.0f; // Thời gian để chuyển

    private void Start()
    {
        if (fadeImage != null)
        {

            StartCoroutine(FadeIn());
        }
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);


        yield return new WaitForSeconds(0.1f);

        StartCoroutine(FadeIn());
    }
}