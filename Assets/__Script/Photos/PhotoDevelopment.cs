using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PhotoDevelopment : MonoBehaviour
{
    public Button Developpement;
    public List<char> QTELetters;
    public Image QTEImage;
    public TMP_Text TimeText;
    public TMP_Text TimeTextShadow;
    public TMP_Text QTEText;
    public Image QTESpriteTime;
    public float QTETime;
    public int TimeToDev;
    [HideInInspector] public int Selected = 0;
    int _timeToWithdraw = 0;
    float _failedAttempt;
    [HideInInspector] public int ChangingTime = 0;


    void Start()
    {
        float minutes = Mathf.FloorToInt(_timeToWithdraw / 60);
        float seconds = Mathf.FloorToInt(_timeToWithdraw % 60);

        string text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimeText.text = text;
        TimeTextShadow.text = text;
    }

    void Update()
    {
        if (Selected > 0)
        {
            Developpement.gameObject.SetActive(true);
        }

        else if (Selected <= 0)
        {
            Developpement.gameObject.SetActive(false);
        }

        if (_timeToWithdraw < ChangingTime)
        {
            _timeToWithdraw += (int)Time.deltaTime * 5;
            ChangeTimeToDev();
        }
        else if (_timeToWithdraw > ChangingTime)
        {
            _timeToWithdraw -= (int)Time.deltaTime * 5;
            ChangeTimeToDev();
        }
    }

    public void QTE()
    {
    }

    public void DevPhotos()
    {
        MainGame.Instance.m_Timer.TimeLeft -= (_timeToWithdraw * Selected + _failedAttempt);
    }


    public void ChangeTimeToDev()
    {
        float minutes = ChangingTime / 60;
        float seconds = ChangingTime % 60;

        string text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimeText.text = text;
        TimeTextShadow.text = text;
    }
}