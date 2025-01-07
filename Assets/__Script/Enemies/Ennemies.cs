using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Ennemies : MonoBehaviour
{
    public bool CanMove;
    public List<GizmoToMove> GizmosMovement;
    
    [SerializeField] private float _speed;
    [SerializeField] private Collider _playerCollider;
    
    private bool _hasDetectedPlayer = false;
    private bool _onFirstRound = true;
    private GameObject _target;
    private int _currentWaypointIndex = 0; 
    
    void Update()
    {
        if (_hasDetectedPlayer)
        {
            Debug.Log("Le joueur a été détecté.");
            MainGame.Instance.m_PlayerController.transform.position = MainGame.Instance.RespawnPosition;
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
        if (other == _playerCollider)
        {
            MainGame.Instance.FadingRed.enabled = true;
            MainGame.Instance.FadingRed.DOFade(1, 0.5f);
            StartCoroutine(WaitToRespawn());
        }
            
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(0.5f);
        MainGame.Instance.FadingRed.DOFade(0.5f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        MainGame.Instance.FadingRed.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        MainGame.Instance.FadingRed.DOFade(0.5f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        MainGame.Instance.FadingRed.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        MainGame.Instance.m_PlayerController.transform.position = MainGame.Instance.RespawnPosition;
        MainGame.Instance.FadingRed.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        MainGame.Instance.FadingRed.enabled = false;
    }
    
}
