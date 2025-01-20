using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
//using static UnityEditor.SceneView;

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

        if (!CanMove)
        {
            // set false all deplacement or crouch anim when the player can't move
            if (playerAnim.GetBool("iswalking") == true || playerAnim.GetBool("iswalking") == true || playerAnim.GetBool("iscrouch") == true || playerAnim.GetBool("iscrouchwalk") == true || playerAnim.GetBool("isrunning") == true)
            {
                playerAnim.SetBool("iswalking", false);
                playerAnim.SetBool("iscrouch", false);
                playerAnim.SetBool("iscrouchwalk", false);
                playerAnim.SetBool("isrunning", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            Movement();
        }
    }


    void HandleInput()
    {
        _speed = WalkSpeed;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            /*if (IsCrouching)
            { 
                IsCrouching = false;
                playerAnim.SetBool("iscrouchwalk", false);
            }*/

            //else
            //{
                playerAnim.SetBool("iscrouchwalk", true);
                IsCrouching = true;
                _speed = CrouchSpeed;
            //}
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouching = false;
            playerAnim.SetBool("iscrouchwalk", false);
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
        if (MainGame.Instance.m_Photography.IsActive == false)
        {
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            float newYRotation = Camera.gameObject.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
        }
    }
}