using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Space (10)][Header ("Component")]
    public Transform LookAt;
    public Transform Player;

    [Space(10)][Header("Value")]
    public float YMin = -30.0f;
    public float YMax = 30.0f;
    public float distance = 10.0f;
    public float sensivity = 4.0f;
    private float currentX, currentY = 0.0f;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += -Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = LookAt.position + rotation * Direction;

        transform.LookAt(LookAt.position);

    }

   
}
