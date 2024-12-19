using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Picture : MonoBehaviour
{
    [SerializeField]Outline _outline;

    void Start()
    {
        _outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        if (_outline.enabled==false)
        {
            _outline.enabled = true;
            MainGame.Instance.m_PhotoDevelopment.Selected++;
            MainGame.Instance.m_PhotoDevelopment.ChangingTime += MainGame.Instance.m_PhotoDevelopment.TimeToDev;
        }
        else if (_outline.enabled == true)
        {
            _outline.enabled = false;
            MainGame.Instance.m_PhotoDevelopment.Selected--;
            MainGame.Instance.m_PhotoDevelopment.ChangingTime -= MainGame.Instance.m_PhotoDevelopment.TimeToDev;
        }
    }
}
