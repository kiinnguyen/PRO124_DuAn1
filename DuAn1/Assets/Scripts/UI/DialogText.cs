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
            if (indexText == listText.Count - 1)
            {
                gameObject.SetActive(false);
            }
            else if 
            (indexText < listText.Count)
            {
                indexText++;
                dialogText.text = listText[indexText];
            }
        }
    }

    public void AddNewText(List<string> newText)
    {
        listText = newText;
        indexText = 0;
        dialogText.text = listText[indexText];
    }
}
