using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    private bool _hasDetectedPlayer = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasDetectedPlayer)
        {
            Debug.Log("The player has been detected.");
            _hasDetectedPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == MainGame.Instance.m_PlayerController.GetComponent<Collider>())
        {
            _hasDetectedPlayer = true;
        }
    }
}
