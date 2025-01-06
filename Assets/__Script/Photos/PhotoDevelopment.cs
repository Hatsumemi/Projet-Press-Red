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
    
    float _timeToWithdraw = 0;
    [SerializeField]float _malusTime;
    [HideInInspector] public int ChangingTime = 0; 
    public List<Picture> PicSlected;


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
        if (PicSlected.Count > 0)
        {
            Developpement.gameObject.SetActive(true);
        }

        else if (PicSlected.Count <= 0)
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
        QTEObject.SetActive(true);
    }

    public void DevPhotos()
    {
        MainGame.Instance.m_Timer.TimeLeft -= (_timeToWithdraw * PicSlected.Count + _malusTime * MainGame.Instance.m_QTE.NumberFailed);
        MainGame.Instance.DeveloppmentIsActive = !MainGame.Instance.DeveloppmentIsActive;
        MainGame.Instance.DeveloppmentObj.SetActive(MainGame.Instance.DeveloppmentIsActive);
        foreach ( var i in PicSlected)
        {
            foreach (var image in MainGame.Instance.m_Diary.PicturesDev)
            {
                if (image.sprite == null)
                    image.sprite = i._image.sprite;
                break;
            }
            if (i.PicOutline.enabled == true)
                i.PicOutline.enabled = false;
        }
        PicSlected.Clear();
        QTEObject.SetActive(false);
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