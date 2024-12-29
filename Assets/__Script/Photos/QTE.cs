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
    private void Start()
    {
        _qteCount = QTELetters.Count;
        _qteTimeMax = QTETime;
    }


    void Update()
    {
        QTETime -= Time.deltaTime;
        QTESpriteTime.fillAmount = QTETime / 2;
        for (int i = 0; i < QTELetters.Count; i++)
        {
            if (Input.anyKeyDown)
            {
                if (Input.inputString == QTELetters[i].ToString())
                {
                    Debug.Log(QTELetters[i]);
                    QTETime = 0;
                }

            }

            _qteCount--;
        }
    }
}