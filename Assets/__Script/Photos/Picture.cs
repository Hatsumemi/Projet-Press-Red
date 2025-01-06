using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Picture : MonoBehaviour
{
    public Outline PicOutline;
    [HideInInspector]public Image _image;

    void Start()
    {
        PicOutline.enabled = false;
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        if (PicOutline.enabled==false && _image.sprite != null)
        {
            PicOutline.enabled = true;
            //MainGame.Instance.m_PhotoDevelopment.ChangingTime += MainGame.Instance.m_PhotoDevelopment.TimeToDev;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Add(this);
        }
        else if (PicOutline.enabled == true)
        {
            PicOutline.enabled = false;
            //MainGame.Instance.m_PhotoDevelopment.ChangingTime -= MainGame.Instance.m_PhotoDevelopment.TimeToDev;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Remove(this);
        }
    }
}
