using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeathButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image hoverImage;
    void Start()
    {
        if (hoverImage != null)
        {
            hoverImage.gameObject.SetActive(false); // Đảm bảo hình ảnh ban đầu không hiển thị
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverImage != null)
        {
            hoverImage.gameObject.SetActive(true); // Hiển thị hình ảnh khi hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverImage != null)
        {
            hoverImage.gameObject.SetActive(false); // Ẩn hình ảnh khi thoát hover
        }
    }


    public void ResetGame()
    {
        GameManager.Instance.RestartLevel();
    }

    public void ReturnMenu()
    {
        GameManager.Instance.LoadScene("Menu");
    }
}
