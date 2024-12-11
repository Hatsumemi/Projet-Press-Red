using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [Space(10)]
    [Header("Component")]
    public Transform LookAt;
    public Transform Player;

    [Space(10)]
    [Header("Global Value")]
    public float ChangingDuration; //duration to change between cam TPP and cam FPP
    private float currentX, currentY = 0.0f;
    [HideInInspector] public float YMin, YMax;
    [HideInInspector] public float Distance;
    [HideInInspector] public float Sensivity;
    [HideInInspector] public Camera FOV;

    [Space(10)]
    [Header("Value TPP")]
    public float YMinTPP;
    public float YMaxTPP;
    public float distanceTPP;
    public float sensivityTPP;
    public float FOVTPP;

    [Space(10)]
    [Header("Value FPP")]
    public float YMinFPP;
    public float YMaxFPP;
    public float distanceFPP;
    public float sensivityFPP;
    public float FOVFPP;

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
        currentX += Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
        currentY += -Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -Distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = LookAt.position + rotation * Direction;

        transform.LookAt(LookAt.position);

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
}
