using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Picture : MonoBehaviour
{
    [FormerlySerializedAs("Outline")] [FormerlySerializedAs("_outline")] public Outline PicOutline;

    void Start()
    {
        PicOutline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        if (PicOutline.enabled==false)
        {
            PicOutline.enabled = true;
            MainGame.Instance.m_PhotoDevelopment.ChangingTime += MainGame.Instance.m_PhotoDevelopment.TimeToDev;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Add(this);
        }
        else if (PicOutline.enabled == true)
        {
            PicOutline.enabled = false;
            MainGame.Instance.m_PhotoDevelopment.ChangingTime -= MainGame.Instance.m_PhotoDevelopment.TimeToDev;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Remove(this);
        }
    }
}
