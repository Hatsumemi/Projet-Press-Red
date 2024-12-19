using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] TMP_Text _timerTextShadow;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeft > 0)
        {
            if (MainGame.Instance.DeveloppmentIsActive == false)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
        }
        else
        {
            Time.timeScale = 0;
            updateTimer(0);
            Debug.Log("Time has ended. You've failed your mission.");
        }
    }

    public void updateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        string text = string.Format("{0:00} : {1:00}", minutes, seconds);

        _timerText.text = text;
        _timerTextShadow.text = text;
    }
}