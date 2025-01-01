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
        _qteCount = QTELetters.Count;
    }


    void Update()
    {
        QTETime -= Time.deltaTime;
        QTESpriteTime.fillAmount = QTETime / _qteTimeMax;
        if (_qteCount > 0)
        {
            if (QTETime > 0)
                QTENext(_qteCount - 1);
            if (QTETime <= 0)
                QTETime = _qteTimeMax;
        }

        else
        {
            MainGame.Instance.DeveloppmentObj.SetActive(false);
            _qteCount = QTELetters.Count;
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
                _qteCount--;
            }
        }
    }
}