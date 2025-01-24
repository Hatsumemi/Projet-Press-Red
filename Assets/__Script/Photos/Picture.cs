using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Picture : MonoBehaviour
{
    public List<bool> HasObjectivesIn;
    public Outline PicOutline;
    [FormerlySerializedAs("_image")] [HideInInspector]public Image Image;


    private void Awake()
    {
        for (int i = 0; i < 3; i++) //c'est plus simple comme ça et c'est plus bo
            HasObjectivesIn.Add(false);
    }

    void Start()
    {
       
        if (PicOutline != null)
            PicOutline.enabled = false;
        Image = GetComponent<Image>();
    }

    void Update()
    {
       
    }

    public void OnClick()
    {
        if (PicOutline.enabled==false && Image.sprite != null)
        {
            PicOutline.enabled = true;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Add(this);
        }
        else if (PicOutline.enabled == true)
        {
            PicOutline.enabled = false;
            MainGame.Instance.m_PhotoDevelopment.PicSlected.Remove(this);
        }
    }
}
