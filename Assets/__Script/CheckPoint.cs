using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == MainGame.Instance.m_PlayerController.GetComponent<Collider>())
        {
            MainGame.Instance.RespawnPosition = transform.position;
        }
    }
}
