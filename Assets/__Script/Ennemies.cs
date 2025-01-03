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
    private bool _onFirstRound = true;
    private GameObject _target;
    private int _currentWaypointIndex = 0; 
    
    void Update()
    {
        if (_hasDetectedPlayer)
        {
            Debug.Log("Le joueur a été détecté.");
            _hasDetectedPlayer = false;
        }

        if (CanMove)
        {
            MoveTowardsWaypoint();
        }
    }

    private void MoveTowardsWaypoint()
    {
        if (_currentWaypointIndex < 0)
            _currentWaypointIndex = 0;
        else if (_currentWaypointIndex == GizmosMovement.Count)
            _currentWaypointIndex = GizmosMovement.Count-1;
        _target = GizmosMovement[_currentWaypointIndex].gameObject;

            if (Vector3.Distance(transform.position, _target.transform.position) > 0.1f)
            {
                float step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
            }
            else
            {
                if (_onFirstRound)
                {
                    _currentWaypointIndex++; 
                    if (_currentWaypointIndex == GizmosMovement.Count)
                    {
                        _onFirstRound = false; 
                    }
                }
                else if (_onFirstRound == false)
                {
                    _currentWaypointIndex--; 
                    if (_currentWaypointIndex == 0) 
                    {
                        _onFirstRound = true; 
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
