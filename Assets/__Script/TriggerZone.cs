using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Types Type;
    public GameObject Player;
    public enum Types
    {
        Photography,
        Development,
    }


    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject == Player)
        {
            if (Type == Types.Photography)
            {
                Debug.Log("Hiiii");
                MainGame.Instance.m_Photography.Triggered = true;
                MainGame.Instance.TextPhoto.gameObject.SetActive(true);
            }

            if (Type == Types.Development)
            {
                Debug.Log("Hellow");
                MainGame.Instance.Triggered = true;
                MainGame.Instance.TextDev.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        MainGame.Instance.m_Photography.Triggered = false;
        MainGame.Instance.Triggered = false;
        MainGame.Instance.TextPhoto.gameObject.SetActive(false);
        MainGame.Instance.TextDev.gameObject.SetActive(false);
    }
}
