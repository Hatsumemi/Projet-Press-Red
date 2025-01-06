using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Diary : MonoBehaviour
{
    public List<Image> PicturesDev;
    public Button ValidatingButton;
    public Image Objective;
    

    public void CheckImages()
    {
        foreach (var image in PicturesDev)
        {
            if (image.sprite != null)
            {
                image.gameObject.SetActive(true);
            }
        }
    }

    public void OkCheck()
    {
        MainGame.Instance.Fading.DOFade(1, 2);
    }
}
