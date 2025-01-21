using System;
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
    public List<Image> Images;
    public List<Picture> PicSlected;


    private void Start()
    {
        foreach (Image image in Images)
        {
            if(image.sprite == null)
                image.gameObject.SetActive(false);
            else
            {
                image.gameObject.SetActive(true);
            }
        }
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
    }

    public void QTE()
    {
        QTEObject.SetActive(true);
    }

    public void DevPhotos()
    {
        MainGame.Instance.DeveloppmentIsActive = false;
        MainGame.Instance.DeveloppmentObj.SetActive(MainGame.Instance.DeveloppmentIsActive);
        foreach (var i in PicSlected)
        {
            foreach (var image in MainGame.Instance.m_Diary.Pages[MainGame.Instance.m_Diary.PageOn].GetComponent<DiaryMission>().PicturesDev)
            {
                if (image.sprite == null)
                {
                    image.sprite = i.Image.sprite;
                    if (i.HasObjectiveIn)
                        image.GetComponent<Picture>().HasObjectiveIn = true;
                    i.Image.sprite = null;
                    break;
                }
            }
            if (i.PicOutline.enabled == true)
                i.PicOutline.enabled = false;
        }
        PicSlected.Clear();
        MainGame.Instance.m_PlayerController.CanMove = true;
        QTEObject.SetActive(false);
    }
    
}