using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoDevelopment : MonoBehaviour
{
    public TMP_Text TimeToDev;
    public Button Developpement;
    public List<char> QTELetters;
    public Image QTEImage;
    public TMP_Text QTEText;
    public float QTETime;
    public Image QTESpriteTime;
    [HideInInspector]public int Selected = 0;
    float _timeToWithdraw;
    float _failedAttempt;
    



    void Start()
    {
        
    }

    void Update()
    {
        if (Selected > 0)
            Developpement.enabled = true;
        if (Selected <= 0)
            Developpement.enabled = false;
    }
    
    public void QTE()
    {

    }

    public void DevPhotos()
    {
        MainGame.Instance.m_Timer.TimeLeft -= (_timeToWithdraw + _failedAttempt);
    }
}
