using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpeed : MonoBehaviour
{
    private int timeScale = 1;
    [SerializeField] private Text timeSpeed;

    public void SpeedChange()
    {
        timeScale += 1;
        if (timeScale == 4)
            timeScale = 1;

        Time.timeScale = timeScale;

        timeSpeed.text =timeScale.ToString();
    }
}
