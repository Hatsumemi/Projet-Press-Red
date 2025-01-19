using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Picture : MonoBehaviour
{
    public bool HasObjectiveIn = false;
    public Outline PicOutline;
    [FormerlySerializedAs("_image")] [HideInInspector]public Image Image;

    void Start()
    {
        if(PicOutline != null)
            PicOutline.enabled = false;
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        if (PicOutline.enabled==false && Image.sprite != null)
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
