using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeadDialog : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image container;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Text text2;

    private bool isFade = false;
    private bool isText = false;
    private float originalFontSize;

    void Start()
    {
        GameManager.Instance.PauseGame();
        if (text != null && container != null)
        {
            originalFontSize = text.fontSize;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
            StartCoroutine(DeadDialogCoroutine());
        }
    }

    IEnumerator DeadDialogCoroutine()
    {
        // Giai đoạn 1: Chuyển màu và tăng kích thước chữ
        float duration = 2f;
        float elapsedTime = 0f;
        Vector3 originalPosition = text.transform.localPosition;
        float moveDistance = 200f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * 0.3f;
            float t = elapsedTime / duration;

            // Chuyển màu từ trong suốt sang màu gốc
            Color colorContainer = container.color;
            Color colorText = text.color;
            colorContainer.a = Mathf.Lerp(0f, 1f, t);
            colorText.a = Mathf.Lerp(0f, 1f, t);
            container.color = colorContainer;
            text.color = colorText;

            // Tăng kích thước chữ
            text.fontSize = (int)Mathf.Lerp(originalFontSize, originalFontSize + 50f, t);

            yield return null;
        }

        // Đảm bảo màu và kích thước chữ đạt giá trị cuối cùng
        container.color = new Color(container.color.r, container.color.g, container.color.b, 1f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        text.fontSize = (int)originalFontSize + 50;

        // Giai đoạn 2: Di chuyển chữ lên trên
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 0.3f;
            float t = elapsedTime / 1f;

            text.transform.localPosition = Vector3.Lerp(originalPosition, originalPosition + new Vector3(0, moveDistance, 0), t);
            yield return null;
        }

        // Giai đoạn 3: Hiển thị các nút
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);

        yield return null;
    }

}
