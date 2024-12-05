using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera TPP;
    public Camera FPP;
    public List<Camera> Cameras;

    private Camera _currentCam;

    void Start()
    {
        FPP.enabled = false;
        TPP.enabled = true;
    }

    public void ChangeCam()
    {
        foreach (var cam in Cameras)
        {
            if (!cam.enabled)
            {
                cam.enabled = true;
                _currentCam = cam;
            }
            else cam.enabled = false;
        }
    }
}
