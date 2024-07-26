using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Slider slider;
  
    public void UpdateHeathBar(float currenValue, float maxValue)
    {
        slider.value = currenValue / maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
