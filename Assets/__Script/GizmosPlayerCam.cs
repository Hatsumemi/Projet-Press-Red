using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPlayerCam : MonoBehaviour
{
    public float GizmosY;

    void Update()
    {
        transform.position = new Vector3(PlayerController.Instance.transform.position.x, 
            PlayerController.Instance.transform.position.y + GizmosY, 
            PlayerController.Instance.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
