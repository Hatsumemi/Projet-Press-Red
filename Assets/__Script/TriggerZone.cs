using System.Collections;
using System.Collections.Generic;
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
            }

            if (Type == Types.Development)
            {
                Debug.Log("Hellow");
                MainGame.Instance.Triggered = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        MainGame.Instance.m_Photography.Triggered = false;
    }
}
