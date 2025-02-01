using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Threading;
using UnityEngine.UIElements;

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

    private Vector3 _adjustedPosition;

    private Ray ray2;

    [Space(10)] [Header("Changing Angle for Photography")]
    public float AngleZ;

    public float MaxAngle;
    public float MinAngle;

    float differenceX;
    float differenceY;

    bool _camInWall = false;

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
            float previousX = currentX;
            float previousY = currentY;

            currentX += Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
            currentY += -Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;

            currentY = Mathf.Clamp(currentY, YMin, YMax);

            differenceX = currentX - previousX;
            differenceY = currentY - previousY;


            //if ((differenceX > 0 || differenceX < 0 || differenceY > 0 || differenceY < 0))
            //{
            //if (_camInWall == false)
            //{
            Debug.Log("Camera mooving");
            Vector3 Direction = new Vector3(0, 0, -Distance);
            if (MainGame.Instance.m_Photography.IsActive)
            {
                Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
                transform.position = LookAt.position + rotation * Direction;
                transform.LookAt(LookAt.position,
                    new Vector3(
                        Mathf.Cos(Mathf.Deg2Rad * AngleZ),
                        Mathf.Sin(Mathf.Deg2Rad * AngleZ), 0));
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
                transform.position = LookAt.position + rotation * Direction;
                transform.LookAt(LookAt.position);
            }
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
        if (MainGame.Instance.m_Photography.IsActive == false) //(differenceX != 0 || differenceY != 0))
        {
            if (!LookAt)
                return;

            Vector3 desiredPosition = LookAt.position - transform.forward * Distance;
            Vector3 direction = (desiredPosition - LookAt.position).normalized;
            RaycastHit hit;

            RaycastHit hit2;
            RaycastHit hit3;

            if (Physics.Raycast(LookAt.position, direction, out hit, Distance, LayerMask.GetMask("Default"),
                    QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject != LookAt.gameObject && hit.transform.gameObject != Player.gameObject &&
                    _camInWall) //== false)
                {
                    _camInWall = true;
                    Debug.Log("CAMERA IN WALL");
                    Distance = Mathf.Clamp(hit.distance, 0.5f, distanceTPP);
                    _adjustedPosition = LookAt.position - direction * Distance;

                    Transform headTransform = LookAt.gameObject.GetComponent<GizmosPlayerCam>().head;
                    LookAt.gameObject.transform.position = new Vector3(headTransform.position.x,
                        headTransform.position.y, headTransform.position.z);
                }
            }
            else if (Physics.Raycast(transform.position, transform.right, out hit2, 1, LayerMask.GetMask("Default"),
                         QueryTriggerInteraction.Ignore))
            {
                if (hit2.transform.gameObject != LookAt.gameObject &&
                    hit2.transform.gameObject != Player.gameObject) //&& _camInWall == false)
                {
                    Debug.Log("CAMERA IN WALL");

                    _camInWall = true;
                    Distance = Mathf.Clamp(hit.distance, 0.5f, distanceTPP);
                    _adjustedPosition = LookAt.position - direction * Distance;

                    Transform headTransform = LookAt.gameObject.GetComponent<GizmosPlayerCam>().head;
                    LookAt.gameObject.transform.position = new Vector3(headTransform.position.x,
                        headTransform.position.y, headTransform.position.z);
                }
            }
            else if (Physics.Raycast(transform.position, -transform.right, out hit3, 1, LayerMask.GetMask("Default"),
                         QueryTriggerInteraction.Ignore))
            {
                if (hit3.transform.gameObject != LookAt.gameObject &&
                    hit3.transform.gameObject != Player.gameObject) //&& _camInWall == false)
                {
                    _camInWall = true;
                    Debug.Log("CAMERA IN WALL");
                    Distance = Mathf.Clamp(hit.distance, 0.5f, distanceTPP);
                    _adjustedPosition = LookAt.position - direction * Distance;

                    Transform headTransform = LookAt.gameObject.GetComponent<GizmosPlayerCam>().head;
                    LookAt.gameObject.transform.position = new Vector3(headTransform.position.x,
                        headTransform.position.y, headTransform.position.z);
                }
            }
            else
            {
                Debug.Log("CAMERA OUT WALL");
                _camInWall = false;
                Distance = Mathf.Lerp(Distance, distanceTPP, Time.deltaTime * 5f);
                _adjustedPosition = desiredPosition;
            }


            Vector3 newPos = Vector3.Lerp(transform.position, _adjustedPosition, Time.deltaTime * 10f);

            float distanceActualPosAndNewPos =
                Vector3.Distance(newPos, transform.position); // a poursuivre ou a retirer

            if (distanceActualPosAndNewPos > 10)
            {
                transform.position = newPos;
            }

            transform.LookAt(LookAt.position);

            //RaycastHit hit2;
            //RaycastHit hit3;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LookAt.position,
            LookAt.position + (transform.position - LookAt.position).normalized * Distance);

        Gizmos.DrawLine(transform.position, transform.position + transform.right * 1);
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * 1);
    }
}