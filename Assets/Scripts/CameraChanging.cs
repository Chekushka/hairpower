using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanging : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    private CinemachineVirtualCamera _currentCamera;
   
    public void ChangeCamera(CameraType cameraType)
    {
        SetCameraPrioritiesToZero();
        cameras[(int)cameraType].Priority = 1;
        _currentCamera = cameras[(int)cameraType];
    }

    public void FreezeCamera()
    {
        _currentCamera.m_Follow = null;
        _currentCamera.m_LookAt = null;
    }

    private void SetCameraPrioritiesToZero()
    {
        foreach (var cam in cameras)
            cam.Priority = 0;
    }
}

public enum CameraType
{
    Main,
    Finish,
    Attack,
    Hair
}
