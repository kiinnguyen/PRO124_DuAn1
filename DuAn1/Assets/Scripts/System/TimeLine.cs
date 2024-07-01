using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour
{

    public Text timerText;

    private bool isRunning = true;

    private float seconds;
    private float mins;
    private float hours;
    private float days;
    public float tick;




    private void Start()
    {
    }

    void Update()
    {
        if (isRunning)
        {
            seconds += Time.fixedDeltaTime * tick;

            if (seconds >= 60) // 1 min = 60 seconds
            {
                seconds = 0;
                mins++;   
            }

            if (mins >= 60) // 1 hour = 60 mins
            {
                mins = 0;
                hours++;
            }

            if (hours >= 24) // 1 day = 24 hours
            {
                hours = 0;
                days++;
            }
        }
    }

    private void FixedUpdate()
    {
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, mins, (int)seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }
}
