using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPlayerCam : MonoBehaviour
{
    public float GizmosX;
    public float GizmosY;
    public float GizmosZ;

    void Update()
    {
        transform.position = new Vector3(MainGame.Instance.m_PlayerController.transform.position.x + GizmosX,
            MainGame.Instance.m_PlayerController.transform.position.y + GizmosY,
            MainGame.Instance.m_PlayerController.transform.position.z + GizmosZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
