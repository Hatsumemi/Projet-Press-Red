using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

public class IA_Pathing : MonoBehaviour
{
    public NavMeshAgent agent;
    //public Transform player;
    public LayerMask WhatIsGround; //, whatIsPlayer;

    //public Vector3 walkPoint;
    //bool walkPointSet;
    
    //public float timeBetweenAttacks;
    //private bool alreadyAttacked;

    /*
    [Header("Ranges")]
    public float sightRange, attackRange;
    private float startSightRange;
    private bool playerInSightRange, playerInAttackRange;
    */

    [Header("Path timer")]
    public float pathUpdateDelay = 0.2f;
    private float timer = 0f;
    
    public float moveSpeed;

    [Header("Patrols Points")]
    [SerializeField] private List<GameObject> patrolsPointsGameObjects;
    [SerializeField] private List<GameObject> VisitedPatrolsPoints;
    private GameObject closestPatrolPoint = null;
    private bool arrivedPatrolPoint = true;


    [SerializeField] private bool _closestPointOrder = false;
    [SerializeField] private bool _goAndBackMode = false;
    private int patrolIndex = 0;


    [SerializeField] private bool _isMainCharacter = false;

    [HideInInspector] public bool CanGoObjective = false;
    [SerializeField] private GameObject _objectivePoint;
    private bool _arrivedOnObjective = false;

    [SerializeField] private GameObject _otherTransactor;

    [HideInInspector] public bool CanExit = false;

    [SerializeField] private GameObject _exitPoint;



    void Awake()
    {
        //player = GameObject.Find("PlayerObj").transform; //FindObjectOfType<PlayerControl>().transform;
        agent = GetComponent<NavMeshAgent>();

        //DetectHidingPoints();
        //DetectPatrolPoints();

        //startSightRange = sightRange;
    }



    private void Update()
    {
        timer += Time.deltaTime;

        if (CanGoObjective == false && CanExit == false)
        {
            PatrolingPoint();
        }

        if (CanGoObjective == true && CanExit == false)
        {
            GoObjectivePoint();
        }


        if (Vector3.Distance(transform.position, _objectivePoint.transform.position) < 2)
        {
            transform.LookAt(new Vector3( _otherTransactor.transform.position.x, _otherTransactor.transform.position.y, _otherTransactor.transform.position.z));
        }

        if (CanExit == true)
        {
            GoExitPoint();
        }
    }



    /*    private bool isFront()
        {
            Vector3 directonOfPlayer = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, directonOfPlayer);

            if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
            {
                return true;
            }

            return false;
        }
    */


    private void PatrolingPoint()
    {

        if (timer > pathUpdateDelay)
        {
            if (VisitedPatrolsPoints.Count >= patrolsPointsGameObjects.Count)
            {
                VisitedPatrolsPoints.Clear();
            }

            timer = 0f;
            float closestTargetDistance = float.MaxValue;
            NavMeshPath path = new NavMeshPath();

            if (arrivedPatrolPoint == true)
            {
                foreach (GameObject patrolPoint in patrolsPointsGameObjects)
                {
                    if (patrolPoint == null || VisitedPatrolsPoints.Contains(patrolPoint) == true)
                    {
                        continue;
                    }

                    if (_closestPointOrder == true)
                    {
                        if (NavMesh.CalculatePath(transform.position, patrolPoint.transform.position, agent.areaMask, path))
                        {

                            float distance = Vector3.Distance(transform.position, path.corners[0]);

                            for (int j = 1; j < path.corners.Length; j++)
                            {
                                distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                            }

                            if (distance < closestTargetDistance)
                            {

                                closestTargetDistance = distance;
                                closestPatrolPoint = patrolPoint;
                            }
                        }
                    }
                    else if (_closestPointOrder == false)
                    {
                        if (patrolIndex >= patrolsPointsGameObjects.Count)
                        {
                            patrolIndex = 0;
                        }

                        closestPatrolPoint = patrolsPointsGameObjects[patrolIndex];
                        patrolIndex++;
                    }

                }
            }

            GoPatrolPoint();
        }
    }




    private void GoPatrolPoint()
    {
        if (closestPatrolPoint != null)
        {
            agent.SetDestination(closestPatrolPoint.transform.position);
            transform.LookAt(agent.steeringTarget);

            if (Vector3.Distance(transform.position, closestPatrolPoint.transform.position) < 2)
            {
                if(VisitedPatrolsPoints.Contains(closestPatrolPoint) == false)
                {
                    VisitedPatrolsPoints.Add(closestPatrolPoint);
                }
                arrivedPatrolPoint = true;
            }
            else
            {
                arrivedPatrolPoint = false;
            }
        }
    }


    private void GoObjectivePoint()
    {
        agent.SetDestination(_objectivePoint.transform.position);

        if (Vector3.Distance(transform.position, _objectivePoint.transform.position) < 2)
        {
            _arrivedOnObjective = true;
        }
    }


    private void GoExitPoint()
    {
        agent.SetDestination(_exitPoint.transform.position);
        if (Vector3.Distance(transform.position, _exitPoint.transform.position) < 2)
        {
            Destroy(gameObject);
        }
    }
}