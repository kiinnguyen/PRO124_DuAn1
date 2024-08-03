using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogText : MonoBehaviour
{
    [SerializeField] Text dialogText;

    private GameManager gameManager;

    private int indexText;
    private List<string> listText;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (indexText == listText.Count - 1)
            {
                gameObject.SetActive(false);
                GameManager.Instance.ResumeGame();
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
