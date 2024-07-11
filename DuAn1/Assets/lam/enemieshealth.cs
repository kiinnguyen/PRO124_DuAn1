using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieshealth : MonoBehaviour
{
    public int currenHealth;
    public   int maxhealth;
    
    

    private bool flashActive;
    [SerializeField]
    private float flashLength = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer enemypSprite;

    // Start is called before the first frame update
    void Start()
    {
        enemypSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            if (flashCounter > flashLength * .99)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .82)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .66)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .49)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .33)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .16)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 0f);
            }
            else
            {
                enemypSprite.color = new Color(enemypSprite.color.r, enemypSprite.color.g, enemypSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    public void HurtEnemies(int damageToGive)
    {
        currenHealth -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
        if (currenHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
