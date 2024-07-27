using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
    private bool isPlayerInRange = false;
    [SerializeField] private GameObject dialogBanner;
    [SerializeField] private GameObject dialogIcon;
    [SerializeField] private List<string> listTalk;

    private Coroutine iconEffectCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogIcon.SetActive(true);
            if (iconEffectCoroutine != null)
            {
                StopCoroutine(iconEffectCoroutine);
            }
            iconEffectCoroutine = StartCoroutine(IconEffect());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogIcon.SetActive(false);
            if (iconEffectCoroutine != null)
            {
                StopCoroutine(iconEffectCoroutine);
                iconEffectCoroutine = null;
            }
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (listTalk.Count > 0)
            {
                dialogBanner.SetActive(true);
                dialogBanner.GetComponent<DialogText>().AddNewText(listTalk);
            }
        }
    }

    IEnumerator IconEffect()
    {
        Vector3 startPos = dialogIcon.transform.position;
        Vector3 upPos = startPos + Vector3.up * 0.1f;
        Vector3 downPos = startPos + Vector3.down * 0.1f;
        float speed = 1f;

        while (true)
        {
            while (dialogIcon.transform.position != upPos)
            {
                dialogIcon.transform.position = Vector3.MoveTowards(dialogIcon.transform.position, upPos, speed * Time.deltaTime);
                yield return null;
            }

            while (dialogIcon.transform.position != downPos)
            {
                dialogIcon.transform.position = Vector3.MoveTowards(dialogIcon.transform.position, downPos, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}