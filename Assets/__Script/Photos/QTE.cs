using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public float QTETime;
    public List<char> QTELetters;
    public TMP_Text QTEText;
    public Image QTEImage;
    public Image QTESpriteTime;

    int _qteCount;
    float _qteTimeMax;
    private bool _validated;

    [HideInInspector] public int NumberFailed;


    private void Awake()
    {
        _qteTimeMax = QTETime;
    }

    private void Start()
    {
        _qteCount = 0;
    }


    void Update()
    {
        QTETime -= Time.deltaTime;
        QTESpriteTime.fillAmount = QTETime / _qteTimeMax;
        if (_qteCount < QTELetters.Count)
        {
            if (QTETime > 0)
                QTENext(_qteCount);
            if (QTETime <= 0)
                QTETime = _qteTimeMax;
        }

        else
        {
            MainGame.Instance.DeveloppmentObj.SetActive(false);
            _qteCount = 0;
            NumberFailed = 0;
            MainGame.Instance.m_PhotoDevelopment.DevPhotos();
        }
    }


    void QTENext(int i)
    {
        QTEText.text = QTELetters[i].ToString().ToUpper();
        if (Input.anyKeyDown)
        {
            if (Input.inputString == QTELetters[i].ToString())
            {
                Debug.Log(QTELetters[i]);
                QTETime = 0;
                _qteCount++;
            }
            else
            {
                NumberFailed++;
                _qteCount++;
            }
        }
    }
}