using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAndZoomText : MonoBehaviour
{
    public Text textToAnimate; // Text component bạn muốn nhấp nháy và zoom
    public float blinkInterval = 0.5f; // Thời gian giữa các lần nhấp nháy
    public float zoomInterval = 0.5f; // Thời gian giữa các lần zoom
    public float zoomFactor = 1.2f; // Hệ số zoom
    private Vector3 originalScale;

    [SerializeField] GameObject dialogBanner;

    void Start()
    {
        dialogBanner = GameObject.Find("Dialog Talk");
        textToAnimate = GetComponent<Text>();
        if (textToAnimate != null)
        {
            originalScale = textToAnimate.transform.localScale;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            Color currentColor = textToAnimate.color;
            currentColor.a = currentColor.a == 1f ? 0.5f : 1f;
            textToAnimate.color = currentColor;

            yield return new WaitForSeconds(blinkInterval);
        }
    }

}
