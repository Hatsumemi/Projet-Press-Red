using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;

    public int WalkSpeed, RunSpeed;

    int _speed;
    float _horizontalAxis, _verticalAxis;


    private void Update()
    {
        _speed = WalkSpeed;
        if ( Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed = RunSpeed;
        }
        
        //reading the input:
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {


        //assuming we only using the single camera:
        var camera = Camera;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * _verticalAxis + right * _horizontalAxis;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * _speed * Time.deltaTime);

    }
}
