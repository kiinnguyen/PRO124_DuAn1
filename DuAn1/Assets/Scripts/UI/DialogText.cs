using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogText : MonoBehaviour
{
    [SerializeField] Text dialogText;

    private int indexText;
    private List<string> listText;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (listText != null && indexText < listText.Count)
            {
                dialogText.text = listText[indexText];
                indexText++;

                if (indexText == listText.Count)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddNewText(List<string> newText)
    {
        if (newText != null)
        {
            listText = newText;
            indexText = 0;

            if (listText.Count > 0)
            {
                dialogText.text = listText[indexText];
                indexText++;
            }
        }
        else
        {
            dialogText.text = string.Empty;
        }
    }
}
