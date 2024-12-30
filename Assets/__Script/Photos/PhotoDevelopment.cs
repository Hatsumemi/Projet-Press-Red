using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PhotoDevelopment : MonoBehaviour
{
    public Button Developpement;
    public GameObject QTEObject;
    
    public TMP_Text TimeText;
    public TMP_Text TimeTextShadow;
    
    public int TimeToDev;
    
    [HideInInspector] public int Selected = 0;
    float _timeToWithdraw = 0;
    [SerializeField]float _malusTime;
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
            _timeToWithdraw += Time.deltaTime ;
            ChangeTimeToDev();
        }
        else if (_timeToWithdraw > ChangingTime)
        {
            _timeToWithdraw -= Time.deltaTime ;
            ChangeTimeToDev();
        }
    }

    public void QTE()
    {
    }

    public void DevPhotos()
    {
        QTEObject.SetActive(true);
        MainGame.Instance.m_Timer.TimeLeft -= (_timeToWithdraw * Selected + _malusTime * MainGame.Instance.m_QTE.NumberFailed);
        MainGame.Instance.DeveloppmentIsActive = !MainGame.Instance.DeveloppmentIsActive;
        MainGame.Instance.DeveloppmentObj.SetActive(MainGame.Instance.DeveloppmentIsActive);
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