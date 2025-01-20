using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GizmosPlayerCam : MonoBehaviour
{
    public float GizmosX;
    public float GizmosY;
    public float GizmosZ;
    public float GizmosYCrouch;

    [SerializeField] private Transform _head;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private float _distance;

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
            Vector3 newPosition = _head.position + rightDirection * _distance;
            transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * 0.9f);
            //transform.position = newPosition;
        }
        else
        {
            transform.position = new Vector3(_head.position.x, _head.position.y, _head.position.z);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
