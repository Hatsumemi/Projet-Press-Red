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
            HandleInput();
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
                playerAnim.SetBool("iscrouch", false);
            }

            else
            {
                playerAnim.SetBool("iscrouch", true);
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
        //assuming we only using the single camera:
        var camera = Camera;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        //right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * _verticalAxis + right * _horizontalAxis;

        if (desiredMoveDirection != Vector3.zero)
            playerAnim.SetBool("iswalking", true);
        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * _speed * Time.deltaTime);
    }
}