using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    public bool CanMove;
    public List<GizmoToMove> GizmosMovement;
    [SerializeField] private float _speed;
    private bool _hasDetectedPlayer = false;
    private GameObject _target;
    private int _gizmosCount = 0;


    // Update is called once per frame
    void Update()
    {
        if (_hasDetectedPlayer)
        {
            Debug.Log("The player has been detected.");
            _hasDetectedPlayer = false;
        }

        if (CanMove)
        {
            if (_gizmosCount < GizmosMovement.Count)
            {
                for (int i = 0; i < GizmosMovement.Count; i++)
                {
                    _target = GizmosMovement[i].gameObject;
                    if (transform.position == _target.transform.position)
                    {
                        _gizmosCount++;
                    }

                    float step = _speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
                }
            }

            if (_gizmosCount > 0)
            {
                for (int i = GizmosMovement.Count - 1; i > 0; i--)
                {
                    _target = GizmosMovement[i].gameObject;
                    if (transform.position == _target.transform.position)
                    {
                        _gizmosCount--;
                    }

                    float step = _speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
                }
            }
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