using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;


    [Header("Component To Get")] public PlayerController m_PlayerController;
    public CameraManager m_CameraManager;
    public Photography m_Photography;
    public PhotoDevelopment m_PhotoDevelopment;
    public GizmosPlayerCam m_GizmosPlayerCam;
    public Timer m_Timer;
    public QTE m_QTE;

    [Header("Other Variables")]
    [HideInInspector]public Vector3 RespawnPosition;
    [HideInInspector]public bool Triggered = false;
    [HideInInspector]public bool DeveloppmentIsActive = false;
    public GameObject DeveloppmentObj;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_CameraManager.ChangeCam();
        }

        if (Triggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DeveloppmentIsActive = !DeveloppmentIsActive;
                DeveloppmentObj.SetActive(DeveloppmentIsActive);
            }
        }
    }
}