using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;


    [Header("Component To Get")]
    public PlayerController playerController;
    public CameraManager cameraController;

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
            cameraController.ChangeCam();

        }
    }
}
