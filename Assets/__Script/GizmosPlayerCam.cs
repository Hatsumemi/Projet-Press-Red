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
        transform.position = new Vector3(MainGame.Instance.playerController.transform.position.x + GizmosX, 
            MainGame.Instance.playerController.transform.position.y + GizmosY, 
            MainGame.Instance.playerController.transform.position.z + GizmosZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
