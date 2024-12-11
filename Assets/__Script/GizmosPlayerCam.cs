using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPlayerCam : MonoBehaviour
{
    public float GizmosX;
    public float GizmosY;
    public float GizmosZ;
    public float GizmosYCrouch;
    public bool IsCrouch = false;

    void Update()
    {
        if (IsCrouch == false)
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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
