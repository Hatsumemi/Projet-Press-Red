using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GizmosPlayerCam : MonoBehaviour
{
    /*public float GizmosX;
    public float GizmosY;
    public float GizmosZ;
    public float GizmosYCrouch;*/
    public float Distance;
    public float _actualDistance;
    private Vector3 _adjustedPosition;

    [SerializeField] private Transform _head;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private float _cameraDelaySpeed;

    void Update()
    {
        
        /*if (MainGame.Instance.m_PlayerController.IsCrouching == false)
        {
            transform.position = new Vector3(MainGame.Instance.m_PlayerController.transform.position.x + GizmosX,
                MainGame.Instance.m_PlayerController.transform.position.y + GizmosY,
                MainGame.Instance.m_PlayerController.transform.position.z + GizmosZ);

        }
        else
        {
            transform.position = new Vector3(MainGame.Instance.m_PlayerController.transform.position.x + GizmosX,
                MainGame.Instance.m_PlayerController.transform.position.y + GizmosYCrouch,
                MainGame.Instance.m_PlayerController.transform.position.z + GizmosZ);
        }*/



        //if (MainGame.Instance.m_PlayerController.IsCrouching == false)
        //{
            
            /*transform.position = new Vector3(_head.position.x + GizmosX,
                _head.position.y + GizmosY,
            _head.position.z + GizmosZ);*/


            /*if (_head == null || _transformCamera == null)
                return;

            Vector3 rightDirection = _transformCamera.right;
            Vector3 newPosition = _head.position + rightDirection * _distance;
            transform.position = newPosition;*/
        //}
        //else
        //{
            /*transform.position = new Vector3(_head.position.x + GizmosX,
            _head.position.y + GizmosYCrouch,
            _head.position.z + GizmosZ);*/
        //}

        if (MainGame.Instance.m_Photography.IsActive == false)
        {
            if (_head == null || _transformCamera == null)
                return;

            Vector3 rightDirection = _transformCamera.right;
            Vector3 newPosition = _head.position + rightDirection * Distance;
            transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * _cameraDelaySpeed);
            //transform.position = newPosition;
        }
        else
        {
            transform.position = new Vector3(_head.position.x, _head.position.y, _head.position.z);
        }

        GizmoCollide();
    }

    private void GizmoCollide()
    {
        if (MainGame.Instance.m_Photography.IsActive == false)
        {
            if (!_head)
                return;

            Vector3 desiredPosition = _head.position - transform.forward * _actualDistance;
            Vector3 direction = (desiredPosition - _head.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(_head.position, direction, out hit, _actualDistance))
            {
                if (hit.transform.gameObject != _head.gameObject && hit.transform.gameObject != _head.gameObject)
                {
                    Debug.Log("CAMERA IN WALL");
                    _actualDistance = Mathf.Clamp(hit.distance, 0.5f, Distance);
                    _adjustedPosition = _head.position - direction * _actualDistance;
                }
            }
            else
            {
                _actualDistance = Mathf.Lerp(_actualDistance, Distance, Time.deltaTime * 5f);
                _adjustedPosition = desiredPosition;
            }
            transform.position = Vector3.Lerp(transform.position, _adjustedPosition, Time.deltaTime * 10f);

            transform.LookAt(_head.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
