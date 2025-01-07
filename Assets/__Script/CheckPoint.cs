using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Collider _playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == _playerCollider)
        {
            MainGame.Instance.RespawnPosition = transform.position;
        }
    }
}
