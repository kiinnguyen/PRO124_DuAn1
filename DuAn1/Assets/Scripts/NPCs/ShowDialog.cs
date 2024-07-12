using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
    [Header("Dialog Settings")]
    public List<string> dialogLines; // Danh sách các đoạn hội thoại
    public float minTime = 5f; // Thời gian tối thiểu giữa các đoạn hội thoại
    public float maxTime = 10f; // Thời gian tối đa giữa các đoạn hội thoại

    [Header("UI Settings")]
    [SerializeField] TextMeshProUGUI dialogText; // TextMeshProUGUI để hiển thị đoạn hội thoại
    [SerializeField] GameObject dialogContainer;
    private void Start()
    {
        // Bắt đầu coroutine để hiển thị đoạn hội thoại ngẫu nhiên
        StartCoroutine(ShowRandomDialog());
    }

    IEnumerator ShowRandomDialog()
    {
        while (true)
        {
            // Chờ một khoảng thời gian ngẫu nhiên
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            // Chọn một đoạn hội thoại ngẫu nhiên từ danh sách
            string randomDialog = dialogLines[Random.Range(0, dialogLines.Count)];

            // Hiển thị đoạn hội thoại lên UI
            dialogContainer.SetActive(true);
            dialogText.text = randomDialog;

            // Bạn có thể thêm thời gian hiển thị hội thoại, ví dụ: 3 giây
            yield return new WaitForSeconds(3f);

            // Xóa đoạn hội thoại
            dialogContainer.SetActive(false);
            dialogText.text = "";
        }
    }
}
