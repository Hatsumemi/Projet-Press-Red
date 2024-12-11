using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;


    [Header("Component To Get")]
    public PlayerController m_PlayerController;
    public CameraManager m_CameraManager;
    public Photography m_Photography;
    public GizmosPlayerCam m_GizmosPlayerCam;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_GizmosPlayerCam.IsCrouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            m_GizmosPlayerCam.IsCrouch = false;

    }
}
