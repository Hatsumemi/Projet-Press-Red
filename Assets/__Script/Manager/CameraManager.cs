using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour
{
    [Space(10)] [Header("Component")] public Transform LookAt;
    public Transform Player;

    [Space(10)] [Header("Global Value")] public float ChangingDuration; //duration to change between cam TPP and cam FPP
    private float currentX, currentY = 0.0f;
    [HideInInspector] public float YMin, YMax;
    [HideInInspector] public float Distance;
    [HideInInspector] public float Sensivity;
    [HideInInspector] public Camera FOV;

    [Space(10)] [Header("Value TPP")] public float YMinTPP;
    public float YMaxTPP;
    public float distanceTPP;
    public float sensivityTPP;
    public float FOVTPP;

    [Space(10)] [Header("Value FPP")] public float YMinFPP;
    public float YMaxFPP;
    public float distanceFPP;
    public float sensivityFPP;
    public float FOVFPP;

    private Vector3 adjustedPosition;

    void Awake()
    {
        FOV = GetComponent<Camera>();
        YMin = YMinTPP;
        YMax = YMaxTPP;
        Distance = distanceTPP;
        Sensivity = sensivityTPP;
        FOV.fieldOfView = FOVTPP;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (MainGame.Instance.m_PlayerController.CanMove)
        {
            currentX += Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
            currentY += -Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;

            currentY = Mathf.Clamp(currentY, YMin, YMax);

            Vector3 Direction = new Vector3(0, 0, -Distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = LookAt.position + rotation * Direction;

            transform.LookAt(LookAt.position);
        }

        CameraCollide();
    }

    public void ChangeCam()
    {
        float min, max, distance, sensivity, fov;

        MainGame.Instance.m_Photography.IsActive = !MainGame.Instance.m_Photography.IsActive;

        if (MainGame.Instance.m_Photography.IsActive)
        {
            min = YMinFPP;
            max = YMaxFPP;
            distance = distanceFPP;
            sensivity = sensivityFPP;
            fov = FOVFPP;
        }
        else
        {
            min = YMinTPP;
            max = YMaxTPP;
            distance = distanceTPP;
            sensivity = sensivityTPP;
            fov = FOVTPP;
        }

        DOVirtual.Float(YMin, min, ChangingDuration, (f) => YMin = f);
        DOVirtual.Float(YMax, max, ChangingDuration, (f) => YMax = f);
        DOVirtual.Float(Distance, distance, ChangingDuration, (f) => Distance = f);
        DOVirtual.Float(Sensivity, sensivity, ChangingDuration, (f) => Sensivity = f);
        DOVirtual.Float(FOV.fieldOfView, fov, ChangingDuration, (f) => FOV.fieldOfView = f);
    }



    private void CameraCollide()
    {
        if (MainGame.Instance.m_Photography.IsActive == false)
        {
            if (!LookAt)
                return;

            Vector3 desiredPosition = LookAt.position - transform.forward * Distance;
            Vector3 direction = (desiredPosition - LookAt.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(LookAt.position, direction, out hit, Distance))
            {
                if (hit.transform.gameObject != LookAt.gameObject && hit.transform.gameObject != Player.gameObject)
                {
                    Debug.Log("CAMERA IN WALL");
                    Distance = Mathf.Clamp(hit.distance, 0.5f, distanceTPP);
                    adjustedPosition = LookAt.position - direction * Distance;
                }
            }
            else
            {
                Distance = Mathf.Lerp(Distance, distanceTPP, Time.deltaTime * 5f);
                adjustedPosition = desiredPosition;
            }
            transform.position = Vector3.Lerp(transform.position, adjustedPosition, Time.deltaTime * 10f);

            transform.LookAt(LookAt.position);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LookAt.position, LookAt.position + (transform.position - LookAt.position).normalized * Distance);
    }
}