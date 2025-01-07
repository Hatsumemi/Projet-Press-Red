using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerController : MonoBehaviour
{

    public Camera Camera;

    public int WalkSpeed, CrouchSpeed, RunSpeed;

    int _speed;
    float _horizontalAxis, _verticalAxis;
    [HideInInspector] public bool IsCrouching = false;
    [HideInInspector] public bool CanMove = true;

    Animator playerAnim;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        MainGame.Instance.RespawnPosition = transform.position;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleInput();
            
        }
    }

    void FixedUpdate()
    {
        Movement();
    }


    void HandleInput()
    {
        _speed = WalkSpeed;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (IsCrouching)
            { IsCrouching = false;
                playerAnim.SetBool("iscrouchwalk", false);
            }

            else
            {
                playerAnim.SetBool("iscrouchwalk", true);
                IsCrouching = true;
                _speed = CrouchSpeed;
            }
        }

        if (!IsCrouching && Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isrunning", true);
            _speed = RunSpeed;
        }
        else
            playerAnim.SetBool("isrunning", false);

        //reading the input:
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
    }

    void Movement()
    {
        var camera = Camera;

        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        var desiredMoveDirection = forward * _verticalAxis + right * _horizontalAxis;

        if (desiredMoveDirection != Vector3.zero)
            playerAnim.SetBool("iswalking", true);
        else 
            playerAnim.SetBool("iswalking", false);
        if (desiredMoveDirection == Vector3.zero && IsCrouching)
            playerAnim.SetBool("iscrouch", true);
        else
            playerAnim.SetBool("iscrouch", false);

        transform.Translate(desiredMoveDirection * Time.deltaTime * _speed, Space.World);
        RotatePlayerToCamera(desiredMoveDirection);
    }
    
    void RotatePlayerToCamera(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}