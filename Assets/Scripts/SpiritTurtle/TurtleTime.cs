using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurtleTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField]public float remainingTime;

    public void Handle()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
